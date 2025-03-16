namespace Minsk.CodeAnalysis.Binding;

internal sealed class BoundAssignmentExpression(VariableSymbol variable, BoundExpression expression) : BoundExpression
{
    public VariableSymbol Variable { get; } = variable;
    public BoundExpression Expression { get; } = expression;
    public override BoundNodeKind Kind => BoundNodeKind.AssignmentExpression;
    public override Type Type => Expression.Type;
}