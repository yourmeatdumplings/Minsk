namespace Minsk.CodeAnalysis;

public sealed class EvaluationResult(IEnumerable<Diagnostic> diagnostics, object? value)
{
    public IReadOnlyList<Diagnostic> Diagnostics { get; } = diagnostics.ToArray();
    public object? Value { get; } = value;
}