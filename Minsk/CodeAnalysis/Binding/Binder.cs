using Minsk.CodeAnalysis.Syntax;

namespace Minsk.CodeAnalysis.Binding;

internal sealed class Binder(Dictionary<string, object> variables)
{
    private readonly Dictionary<string?, object> _variables = variables;
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
        if (!_variables.TryGetValue(name!, out var value))
        {
            _diagnostics.ReportUndefinedName(syntax.IdentifierToken.Span, name!);
            return new BoundLiteralExpression(0);
        }
        
        var type = value.GetType();
        return new BoundVariableExpression(name, type);
    }

    private BoundExpression BindAssignmentExpression(AssignmentExpressionSyntax syntax)
    {
        var name = syntax.IdentifierToken.Text;
        var boundExpression = BindExpression(syntax.Expression);

        var defaultValue =
            boundExpression.Type == typeof(int)
                ? (object)0
                : boundExpression.Type == typeof(bool)
                    ? false
                    : null;

        _variables[name] = defaultValue ?? throw new Exception($"Unsupported value type {boundExpression.Type}");
        
        return new BoundAssignmentExpression(name, boundExpression);
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