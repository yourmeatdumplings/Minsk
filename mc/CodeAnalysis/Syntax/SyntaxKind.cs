// ReSharper disable once CheckNamespace
namespace Minsk.CodeAnalysis.Syntax;

public enum SyntaxKind
{
    // Tokens
    BadToken,
    EndOfFileToken,
    WhiteSpaceToken,
    NumberToken,
    PlusToken,
    MinusToken,
    StarToken,
    SlashToken,
    OpenParenthesisToken,
    CloseParenthesisToken,
    IdentifierToken,
        
    // keywords
    TrueKeyword,
    FalseKeyword,
    
    // Expressions
    LiteralExpression,
    UnaryExpression,
    BinaryExpression,
    ParenthesizedExpression
}