namespace Minsk.CodeAnalysis.Binding;

internal sealed class BoundAssignmentExpression(string? name, BoundExpression expression) : BoundExpression
{
    public string? Name { get; } = name;
    public BoundExpression Expression { get; } = expression;

    public override BoundNodeKind Kind => BoundNodeKind.AssignmentExpression;
    public override Type Type => Expression.Type;
}