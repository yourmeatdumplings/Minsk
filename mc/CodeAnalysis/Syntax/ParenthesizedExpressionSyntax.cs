namespace Minsk.CodeAnalysis.Syntax
{
    public sealed class ParenthesizedExpressionSyntax(
        SyntaxToken openParenthesisToken,
        ExpressionSyntax expression,
        SyntaxToken closeParenthesisToken) : ExpressionSyntax
    {
        public override SyntaxKind Kind => SyntaxKind.ParenthesizedExpression;
        private SyntaxToken OpenParenthesisToken { get; } = openParenthesisToken;
        public ExpressionSyntax Expression { get; } = expression;
        private SyntaxToken CloseParenthesisToken { get; } = closeParenthesisToken;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return OpenParenthesisToken;
            yield return Expression;
            yield return CloseParenthesisToken;
        }
    }
}