namespace Minsk.CodeAnalysis.Syntax;

public sealed class NameExpressionSyntax(SyntaxToken identifierToken) : ExpressionSyntax
{
    public override SyntaxKind Kind => SyntaxKind.NameExpression;
    public SyntaxToken IdentifierToken { get; } = identifierToken;
    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return IdentifierToken;
    }
}