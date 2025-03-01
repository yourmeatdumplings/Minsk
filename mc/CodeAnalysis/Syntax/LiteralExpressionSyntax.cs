namespace Minsk.CodeAnalysis.Syntax
{
    public sealed class LiteralExpressionSyntax(SyntaxToken numberToken) : ExpressionSyntax
    {
        public override SyntaxKind Kind => SyntaxKind.LiteralExpression;
        public SyntaxToken LiteralToken { get; } = numberToken;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return LiteralToken;
        }
    }
}