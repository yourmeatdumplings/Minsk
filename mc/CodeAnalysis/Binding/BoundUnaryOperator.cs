using Minsk.CodeAnalysis.Syntax;

// ReSharper disable once CheckNamespace
namespace Minsk.CodeAnalysis.Binding;

internal sealed class BoundUnaryOperator
{
    private BoundUnaryOperator(SyntaxKind syntaxKind, BoundUnaryOperatorKind kind, Type operandType)
        : this(syntaxKind, kind, operandType, operandType)
    {
    }

    private BoundUnaryOperator(SyntaxKind syntaxKind, BoundUnaryOperatorKind kind, Type operandType, Type resultType)
    {
        SyntaxKind = syntaxKind;
        Kind = kind;
        OperandType = operandType;
        ResultType = resultType;
    }

    public SyntaxKind SyntaxKind { get; }
    public BoundUnaryOperatorKind Kind { get; }
    public Type OperandType { get; }
    public Type ResultType { get; }

    private static BoundUnaryOperator?[] _operators =
    [
        new(SyntaxKind.BangToken, BoundUnaryOperatorKind.LogicalNegation, typeof(bool)),
        
        new(SyntaxKind.PlusToken, BoundUnaryOperatorKind.Identity, typeof(int)),
        new(SyntaxKind.MinusToken, BoundUnaryOperatorKind.Negation, typeof(int)),
    ];

    public static BoundUnaryOperator? Bind(SyntaxKind syntaxKind, Type operandType)
    {
        return _operators.FirstOrDefault(op => op != null && op.SyntaxKind == syntaxKind && op.OperandType == operandType);
    }
}