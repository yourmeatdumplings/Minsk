namespace Minsk.CodeAnalysis.Syntax;

internal sealed class Lexer(string text)
{
    private readonly string _text = text;
    private int _position;
    private readonly DiagnosticBag _diagnostics = [];

    public DiagnosticBag Diagnostics => _diagnostics;

    private char Current => Peek(0);
    
    private char Lookahead => Peek(1);
    private char Peek(int offset)
    {
        var index = offset + _position;
        return index >= _text.Length ? '\0' : _text[index];
    }

    private void Next()
    {
        _position++;
    }

    public SyntaxToken Lex()
    {
        if (_position >= _text.Length)
            return new SyntaxToken(SyntaxKind.EndOfFileToken, _position, "\0", null);

        var start = _position;
        
        if (char.IsDigit(Current))
        {

            while (char.IsDigit(Current)) Next();

            var length = _position - start;
            var text = _text.Substring(start, length);
            if (!int.TryParse(text, out var value))
                _diagnostics.ReportInvalidNumber(new TextSpan(start, length), _text, typeof(int));
            return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
        }

        if (char.IsWhiteSpace(Current))
        {
            while (char.IsWhiteSpace(Current)) Next();

            var length = _position - start;
            var text = _text.Substring(start, length);
            return new SyntaxToken(SyntaxKind.WhiteSpaceToken, start, text, null);
        }

        if (char.IsLetter(Current))
        {
            while (char.IsLetter(Current)) Next();

            var length = _position - start;
            var text = _text.Substring(start, length);
            var kind = SyntaxFacts.GetKeywordKind(text);
            return new SyntaxToken(kind, start, text, null);
        }

        switch (Current)
        {
            case '+':
                return new SyntaxToken(SyntaxKind.PlusToken, _position++, "+", null);
            case '-':
                return new SyntaxToken(SyntaxKind.MinusToken, _position++, "-", null);
            case '*':
                return new SyntaxToken(SyntaxKind.StarToken, _position++, "*", null);
            case '/':
                return new SyntaxToken(SyntaxKind.SlashToken, _position++, "/", null);
            case '(':
                return new SyntaxToken(SyntaxKind.OpenParenthesisToken, _position++, "(", null);
            case ')':
                return new SyntaxToken(SyntaxKind.CloseParenthesisToken, _position++, ")", null);
            case '&':
                if (Lookahead == '&')
                {
                    _position += 2;
                    return new SyntaxToken(SyntaxKind.AmpersandAmpersandToken, start, "&&", null);
                }
                break;
            case '|':
                if (Lookahead == '|')
                {
                    _position += 2;
                    return new SyntaxToken(SyntaxKind.PipePipeToken, start, "||", null);
                }
                break;
            case '=':
                if (Lookahead == '=')
                {
                    _position += 2;
                    return new SyntaxToken(SyntaxKind.EqualEqualToken, start, "==", null);
                }

                _position += 1;
                return new SyntaxToken(SyntaxKind.EqualsToken, start, "=", null);
            case '!':
                if (Lookahead == '=')
                {
                    _position += 2;
                    return new SyntaxToken(SyntaxKind.BangEqualToken, start, "!=", null);
                }

                _position += 1;
                return new SyntaxToken(SyntaxKind.BangToken, start, "!", null);
        }

        _diagnostics.ReportBadCharacter(_position, Current);
        return new SyntaxToken(SyntaxKind.BadToken, _position++, _text.Substring(_position - 1, 1), null);
    }
}