// ReSharper disable once CheckNamespace
namespace Minsk.CodeAnalysis.Binding;

internal sealed class BoundBinaryExpression(BoundExpression left, BoundBinaryOperator op, BoundExpression right) : BoundExpression
{
    public override BoundNodeKind Kind => BoundNodeKind.UnaryExpression;
    public override Type Type => Op.Type;
    public BoundExpression Left { get; } = left;
    public BoundBinaryOperator Op { get; } = op;
    public BoundExpression Right { get; } = right;

}