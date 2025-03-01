namespace Minsk.CodeAnalysis
{
    public sealed class LiteralExpressionSyntax(SyntaxToken numberToken) : ExpressionSyntax
    {
        public override SyntaxKind Kind => SyntaxKind.NumberExpression;
        public SyntaxToken LiteralToken { get; } = numberToken;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return LiteralToken;
        }
    }
}