using Minsk.CodeAnalysis.Binding;

// ReSharper disable once CheckNamespace
namespace Minsk.CodeAnalysis;

internal sealed class Evaluator(BoundExpression root)
{
    public object Evaluate()
    {
        return EvaluateExpression(root);
    }

    private object EvaluateExpression(BoundExpression node)
    {
        switch (node)
        {
            case BoundLiteralExpression n:
                return n.Value;
            case BoundUnaryExpression u:
            {
                var operand = (int) EvaluateExpression(u.Operand);

                return u.OperatorKind switch
                {
                    BoundUnaryOperatorKind.Identity => operand,
                    BoundUnaryOperatorKind.Negation => -operand,
                    _ => throw new Exception($"Unexpected unary operator {u.OperatorKind}")
                };
            }
            case BoundBinaryExpression b:
            {
                var left = (int) EvaluateExpression(b.Left);
                var right = (int) EvaluateExpression(b.Right);

                return b.OperatorKind switch
                {
                    BoundBinaryOperatorKind.Addition => left + right,
                    BoundBinaryOperatorKind.Subtraction => left - right,
                    BoundBinaryOperatorKind.Multiplication => left * right,
                    BoundBinaryOperatorKind.Division => left / right,
                    _ => throw new Exception($"Unexpected binary operator {b.OperatorKind}")
                };
            }
            default:
                throw new Exception($"Unexpected node {node.Kind}");
        }
    }
}