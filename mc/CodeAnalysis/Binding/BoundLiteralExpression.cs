namespace Minsk.CodeAnalysis.Binding
{
    internal sealed class BoundLiteralExpression(object value) : BoundExpression
    {
        public override BoundNodeKind Kind => BoundNodeKind.LiteralExpression;
        public override Type Type => Value.GetType();
        public object Value { get; } = value;
    }
}