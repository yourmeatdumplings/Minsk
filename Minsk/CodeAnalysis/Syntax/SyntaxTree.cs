namespace Minsk.CodeAnalysis.Syntax;

public sealed class SyntaxTree(IEnumerable<Diagnostic> diagnostics, ExpressionSyntax root, SyntaxToken endOfFileToken)
{
    public IReadOnlyList<Diagnostic> Diagnostics { get; } = diagnostics.ToArray();
    public ExpressionSyntax Root { get; } = root;
    public SyntaxToken EndOfFileToken { get; } = endOfFileToken;

    public static SyntaxTree Parse(string text)
    {
        var parser = new Parser(text);
        return parser.Parse();
    }
    
    public static IEnumerable<SyntaxToken> ParseTokens(string text)
    {
        var lexer = new Lexer(text);
        while (true)
        {
            var token = lexer.Lex();
            if (token.Kind == SyntaxKind.EndOfFileToken)
                break;
            
            yield return token;
        }
    }
}