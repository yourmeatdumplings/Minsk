namespace Minsk.CodeAnalysis.Syntax;

public sealed class AssignmentExpressionSyntax(SyntaxToken identifierToken, SyntaxToken equalsToken, ExpressionSyntax expression) : ExpressionSyntax
{
    public override SyntaxKind Kind => SyntaxKind.AssignmentExpression;
    private SyntaxToken IdentifierToken { get; } = identifierToken;
    private SyntaxToken EqualsToken { get; } = equalsToken;
    private ExpressionSyntax Expression { get; } = expression;

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return IdentifierToken;
        yield return EqualsToken;
        yield return Expression;
    }
}