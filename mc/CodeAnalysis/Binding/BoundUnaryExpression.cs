namespace Minsk.CodeAnalysis.Binding
{
    internal sealed class BoundUnaryExpression(BoundUnaryOperatorKind operatorKind, BoundExpression operand)
        : BoundExpression
    {
        public override BoundNodeKind Kind => BoundNodeKind.UnaryExpression;
        public override Type Type => Operand.Type;
        public BoundUnaryOperatorKind OperatorKind { get; } = operatorKind;
        public BoundExpression Operand { get; } = operand;

    }
}