namespace Minsk.CodeAnalysis.Binding;

internal sealed class BoundVariableExpression(string? name, Type type) : BoundExpression
{
    public override BoundNodeKind Kind => BoundNodeKind.VariableExpression;
    public string? Name { get; } = name;
    public override Type Type { get; } = type;
}