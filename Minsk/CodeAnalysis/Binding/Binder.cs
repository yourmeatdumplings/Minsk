using System.Diagnostics;
using Minsk.CodeAnalysis.Syntax;

namespace Minsk.CodeAnalysis.Binding;

internal sealed class Binder(Dictionary<VariableSymbol, object?> variables)
{
    private readonly Dictionary<VariableSymbol, object?> _variables = variables;
    private readonly DiagnosticBag _diagnostics = [];

    public DiagnosticBag Diagnostics => _diagnostics;
        
    public BoundExpression BindExpression(ExpressionSyntax syntax)
    {
        return syntax.Kind switch
        {
            SyntaxKind.ParenthesizedExpression => BindParenthesizedExpression((ParenthesizedExpressionSyntax)syntax),
            SyntaxKind.LiteralExpression => BindLiteralExpression((LiteralExpressionSyntax)syntax),
            SyntaxKind.NameExpression => BindNameExpression((NameExpressionSyntax)syntax),
            SyntaxKind.AssignmentExpression => BindAssignmentExpression((AssignmentExpressionSyntax)syntax),
            SyntaxKind.UnaryExpression => BindUnaryExpression((UnaryExpressionSyntax)syntax),
            SyntaxKind.BinaryExpression => BindBinaryExpression((BinaryExpressionSyntax)syntax),
            _ => throw new Exception($"Unexpected syntax {syntax.Kind}")
        };
    }

    private BoundExpression BindParenthesizedExpression(ExpressionSyntax syntax)
    {
        return BindExpression(((ParenthesizedExpressionSyntax)syntax).Expression);
    }

    private BoundExpression BindLiteralExpression(LiteralExpressionSyntax syntax)
    {
        var value = syntax.Value ?? 0;
        return new BoundLiteralExpression(value);
    }
    
    private BoundExpression BindNameExpression(NameExpressionSyntax syntax)
    {
        var name = syntax.IdentifierToken.Text;
        var variable = _variables.Keys.FirstOrDefault(v => v.Name == name);

        if (variable != null) return new BoundVariableExpression(variable);
        _diagnostics.ReportUndefinedName(syntax.IdentifierToken.Span, name!);
        return new BoundLiteralExpression(0);

    }

    private BoundExpression BindAssignmentExpression(AssignmentExpressionSyntax syntax)
    {
        var name = syntax.IdentifierToken.Text;
        var boundExpression = BindExpression(syntax.Expression);

        var existingVariable = _variables.Keys.FirstOrDefault(v => v.Name == name);
        if (existingVariable != null)
            _variables.Remove(existingVariable);
        Debug.Assert(name != null, nameof(name) + " != null");
        
        var variable = new VariableSymbol(name, boundExpression.Type);
        _variables[variable] = null;
        
        return new BoundAssignmentExpression(variable, boundExpression);
    }

    private BoundExpression BindUnaryExpression(UnaryExpressionSyntax syntax)
    {
        var boundOperand = BindExpression(syntax.Operand);
        var boundOperator = BoundUnaryOperator.Bind(syntax.OperatorToken.Kind, boundOperand.Type);
        
        if (boundOperator != null) return new BoundUnaryExpression(boundOperator, boundOperand);
        _diagnostics.ReportUndefinedUnaryOperator(syntax.OperatorToken.Span, syntax.OperatorToken.Text, boundOperator!.Type);
        return boundOperand;
    }

    private BoundExpression BindBinaryExpression(BinaryExpressionSyntax syntax)
    {
        var boundLeft = BindExpression(syntax.Left);
        var boundRight = BindExpression(syntax.Right);
        var boundOperator = BoundBinaryOperator.Bind(syntax.OperatorToken.Kind, boundLeft.Type, boundRight.Type);
        if (boundOperator != null) return new BoundBinaryExpression(boundLeft, boundOperator, boundRight);
        _diagnostics.ReportUndefinedBinaryOperator(syntax.OperatorToken.Span, syntax.OperatorToken.Text, boundLeft.Type, boundRight.Type);
        return boundLeft;
    }
}