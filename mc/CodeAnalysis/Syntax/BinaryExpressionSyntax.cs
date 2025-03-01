namespace Minsk.CodeAnalysis.Syntax
{
    public sealed class BinaryExpressionSyntax(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right) : ExpressionSyntax
    {
        public override SyntaxKind Kind => SyntaxKind.BinaryExpression;
        public ExpressionSyntax Left { get; } = left;
        public SyntaxToken OperatorToken { get; } = operatorToken;
        public ExpressionSyntax Right { get; } = right;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Left;
            yield return OperatorToken;
            yield return Right;
        }
    }
}