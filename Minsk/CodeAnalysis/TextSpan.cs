namespace Minsk.CodeAnalysis;

public readonly struct TextSpan(int start, int length)
{
    public int Start { get; } = start;
    public int Length { get; } = length;
    public int End => Start + Length;
}