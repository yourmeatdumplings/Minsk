namespace Minsk.CodeAnalysis.Syntax;

public sealed class LiteralExpressionSyntax(SyntaxToken literalToken, object? value) : ExpressionSyntax
{
    public LiteralExpressionSyntax(SyntaxToken literalToken)
    : this(literalToken, literalToken.Value) { }
    
    public override SyntaxKind Kind => SyntaxKind.LiteralExpression;
    private SyntaxToken LiteralToken { get; } = literalToken;
    public object? Value { get; } = value;

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return LiteralToken;
    }
}