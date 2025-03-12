// ReSharper disable once CheckNamespace
namespace Minsk.CodeAnalysis.Binding;

internal sealed class BoundBinaryExpression(
    BoundExpression left,
    BoundBinaryOperatorKind operatorKind,
    BoundExpression right) : BoundExpression
{
    public override BoundNodeKind Kind => BoundNodeKind.UnaryExpression;
    public override Type Type => Left.Type;
    public BoundExpression Left { get; } = left;
    public BoundBinaryOperatorKind OperatorKind { get; } = operatorKind;
    public BoundExpression Right { get; } = right;

}