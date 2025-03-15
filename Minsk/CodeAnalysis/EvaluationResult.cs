namespace Minsk.CodeAnalysis;

public sealed class EvaluationResult(IEnumerable<string> diagnostics, object? value)
{
    public IReadOnlyList<string> Diagnostics { get; } = diagnostics.ToList();
    public object? Value { get; } = value;
}