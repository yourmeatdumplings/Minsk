using Minsk.CodeAnalysis.Binding;

namespace Minsk.CodeAnalysis;

internal sealed class Evaluator(BoundExpression root, Dictionary<VariableSymbol, object?> variables)
{
    private readonly Dictionary<VariableSymbol, object?> _variables = variables;

    public object? Evaluate()
    {
        return EvaluateExpression(root);
    }

    private object? EvaluateExpression(BoundExpression node)
    {
        switch (node)
        {
            case BoundLiteralExpression n:
                return n.Value;
            case BoundVariableExpression v:
                return _variables[v.Variable];
            case BoundAssignmentExpression a:
            {
                var value = EvaluateExpression(a.Expression);
                _variables[a.Variable] = value;
                return value;
            }
            
            case BoundUnaryExpression u:
            {
                var operand = EvaluateExpression(u.Operand);

                return u.Op.Kind switch
                {
                    BoundUnaryOperatorKind.Identity => (int) operand,
                    BoundUnaryOperatorKind.Negation => -(int) operand,
                    BoundUnaryOperatorKind.LogicalNegation => !(bool) operand,
                    _ => throw new Exception($"Unexpected unary operator {u.Op}")
                };
            }
            case BoundBinaryExpression b:
            {
                var left = EvaluateExpression(b.Left);
                var right = EvaluateExpression(b.Right);

                return b.Op.Kind switch
                {
                    BoundBinaryOperatorKind.Addition => (int) left +(int)  right,
                    BoundBinaryOperatorKind.Subtraction => (int) left - (int) right,
                    BoundBinaryOperatorKind.Multiplication => (int) left * (int) right,
                    BoundBinaryOperatorKind.Division => (int) left / (int) right,
                    BoundBinaryOperatorKind.LogicalAnd => (bool) left && (bool) right,
                    BoundBinaryOperatorKind.LogicalOr => (bool) left || (bool) right,
                    BoundBinaryOperatorKind.Equals => Equals(left, right),
                    BoundBinaryOperatorKind.NotEquals => !Equals(left, right),
                    _ => throw new Exception($"Unexpected binary operator {b.Op}")
                };
            }
            default:
                throw new Exception($"Unexpected node {node.Kind}");
        }
    }
}