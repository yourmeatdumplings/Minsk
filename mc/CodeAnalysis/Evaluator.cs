namespace Minsk.CodeAnalysis
{
    public sealed class Evaluator(ExpressionSyntax root)
    {
        private readonly ExpressionSyntax _root = root;

        public int Evaluate()
        {
            return EvaluateExpression(_root);
        }

        private int EvaluateExpression(ExpressionSyntax node)
        {
            switch (node)
            {
                case LiteralExpressionSyntax n:
                    return (int)(n.LiteralToken.Value ?? throw new InvalidOperationException());
                case BinaryExpressionSyntax b:
                {
                    var left = EvaluateExpression(b.Left);
                    var right = EvaluateExpression(b.Right);

                    return b.OperatorToken.Kind switch
                    {
                        SyntaxKind.PlusToken => left + right,
                        SyntaxKind.MinusToken => left - right,
                        SyntaxKind.StarToken => left * right,
                        SyntaxKind.SlashToken => left / right,
                        _ => throw new Exception($"Unexpected binary operator {b.OperatorToken.Kind}")
                    };
                }
                case ParenthesizedExpressionSyntax p:
                    return EvaluateExpression(p.Expression);
                default:
                    throw new Exception($"Unexpected node {node.Kind}");
            }
        }
    }
}