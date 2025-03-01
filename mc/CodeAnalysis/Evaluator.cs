using Minsk.CodeAnalysis.Binding;
using Minsk.CodeAnalysis.Syntax;

namespace Minsk.CodeAnalysis
{
    internal sealed class Evaluator(BoundExpression root)
    {
        private readonly BoundExpression _root = root;

        public int Evaluate()
        {
            return EvaluateExpression(_root);
        }

        private int EvaluateExpression(BoundExpression node)
        {
            switch (node)
            {
                case BoundLiteralExpression n:
                    return (int)(n.Value ?? throw new InvalidOperationException());
                case BoundUnaryExpression u:
                {
                    var operand = EvaluateExpression(u.Operand);

                    return u.OperatorKind switch
                    {
                        BoundUnaryOperatorKind.Identity => operand,
                        BoundUnaryOperatorKind.Negation => -operand,
                        _ => throw new Exception($"Unexpected unary operator {u.OperatorKind}")
                    };
                }
                case BoundBinaryExpression b:
                {
                    var left = EvaluateExpression(b.Left);
                    var right = EvaluateExpression(b.Right);

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
}