namespace Minsk.CodeAnalysis.Syntax;

public sealed class UnaryExpressionSyntax(SyntaxToken operatorToken, ExpressionSyntax operand) : ExpressionSyntax
{
    public override SyntaxKind Kind => SyntaxKind.UnaryExpression;
    public SyntaxToken OperatorToken { get; } = operatorToken;
    public ExpressionSyntax Operand { get; } = operand;

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return OperatorToken;
        yield return Operand;
    }
}