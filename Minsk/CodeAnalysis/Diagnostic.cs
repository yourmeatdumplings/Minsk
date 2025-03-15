using System.Collections;

namespace Minsk.CodeAnalysis;

public sealed class Diagnostic(TextSpan span, string message)
{
    public TextSpan Span { get; } = span;
    private string Message { get; } = message;

    public override string ToString() => Message;
}