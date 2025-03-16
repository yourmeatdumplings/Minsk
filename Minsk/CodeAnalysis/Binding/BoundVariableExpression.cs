namespace Minsk.CodeAnalysis.Binding;

internal sealed class BoundVariableExpression(VariableSymbol variable) : BoundExpression
{
    public override BoundNodeKind Kind => BoundNodeKind.VariableExpression;
    public VariableSymbol Variable { get; } = variable;
    public override Type Type => Variable.Type;
}