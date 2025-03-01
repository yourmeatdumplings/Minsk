namespace Minsk.CodeAnalysis.Syntax
{
    internal static class SyntaxFacts
    {
        public static int GetUnaryOperatorPrecedence(this SyntaxKind kind)
        {
            return kind switch
            {
                SyntaxKind.PlusToken or SyntaxKind.MinusToken => 3,
                _ => 0
            };
        }
        
        public static int GetBinaryOperatorPrecedence(this SyntaxKind kind)
        {
            return kind switch
            {
                SyntaxKind.StarToken or SyntaxKind.SlashToken => 2,
                SyntaxKind.PlusToken or SyntaxKind.MinusToken => 1,
                _ => 0
            };
        }
    }
}