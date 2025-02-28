namespace Minsk.CodeAnalysis
{
    sealed class NumberExpressionSyntax(SyntaxToken numberToken) : ExpressionSyntax
    {
        public override SyntaxKind Kind => SyntaxKind.NumberExpression;
        public SyntaxToken NumberToken { get; } = numberToken;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return NumberToken;
        }
    }
}