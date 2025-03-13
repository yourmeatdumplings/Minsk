using Minsk.CodeAnalysis.Syntax;

// ReSharper disable once CheckNamespace
namespace Minsk.CodeAnalysis.Binding;

internal sealed class BoundBinaryOperator
{
    private BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind kind, Type type)
        : this(syntaxKind, kind, type, type, type) { }
    
    private BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind kind, Type operandType, Type type)
        : this(syntaxKind, kind, operandType, operandType, type) { }
    
    private BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind kind, Type leftType, Type rightType, Type type)
    {
        SyntaxKind = syntaxKind;
        Kind = kind;
        LeftType = leftType;
        RightType = rightType;
        Type = type;
    }

    private SyntaxKind SyntaxKind { get; }
    public BoundBinaryOperatorKind Kind { get; }
    private Type LeftType { get; }
    private Type RightType { get; }
    public Type Type { get; }

    private static readonly BoundBinaryOperator?[] Operators =
    [
        new(SyntaxKind.PlusToken, BoundBinaryOperatorKind.Addition, typeof(int)),
        new(SyntaxKind.MinusToken, BoundBinaryOperatorKind.Subtraction, typeof(int)),
        new(SyntaxKind.StarToken, BoundBinaryOperatorKind.Multiplication, typeof(int)),
        new(SyntaxKind.SlashToken, BoundBinaryOperatorKind.Division, typeof(int)),
        new(SyntaxKind.EqualEqualToken, BoundBinaryOperatorKind.Equals, typeof(int), typeof(bool)),
        new(SyntaxKind.BangEqualToken, BoundBinaryOperatorKind.NotEquals, typeof(int), typeof(bool)),
        
        new(SyntaxKind.AmpersandAmpersandToken, BoundBinaryOperatorKind.LogicalAnd, typeof(bool)),
        new(SyntaxKind.PipePipeToken, BoundBinaryOperatorKind.LogicalOr, typeof(bool)),
        new(SyntaxKind.EqualEqualToken, BoundBinaryOperatorKind.Equals, typeof(bool)),
        new(SyntaxKind.BangEqualToken, BoundBinaryOperatorKind.NotEquals, typeof(bool))
    ];

    public static BoundBinaryOperator? Bind(SyntaxKind syntaxKind, Type leftType, Type rightType)
    {
        return Operators.FirstOrDefault(op => op != null && op.SyntaxKind == syntaxKind && op.LeftType == leftType && op.RightType == rightType);
    }
}