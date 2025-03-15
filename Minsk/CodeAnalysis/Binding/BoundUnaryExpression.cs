namespace Minsk.CodeAnalysis.Binding;

internal sealed class BoundUnaryExpression(BoundUnaryOperator op, BoundExpression operand)
    : BoundExpression
{
    public override BoundNodeKind Kind => BoundNodeKind.UnaryExpression;
    public override Type Type => Op.Type;
    public BoundUnaryOperator Op { get; } = op;
    public BoundExpression Operand { get; } = operand;

}