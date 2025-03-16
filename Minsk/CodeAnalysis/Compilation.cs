using Minsk.CodeAnalysis.Binding;
using Minsk.CodeAnalysis.Syntax;

namespace Minsk.CodeAnalysis;

public sealed class Compilation(SyntaxTree syntax)
{
    private SyntaxTree Syntax { get; } = syntax;

    public EvaluationResult Evaluate(Dictionary<VariableSymbol, object?> variables)
    {
        var binder = new Binder(variables);
        var boundExpression = binder.BindExpression(Syntax.Root);
        
        var diagnostics = Syntax.Diagnostics.Concat(binder.Diagnostics).ToArray();
        if (diagnostics.Length != 0) return new EvaluationResult(diagnostics, null);
        
        var evaluator = new Evaluator(boundExpression, variables);
        var value = evaluator.Evaluate();
        return new EvaluationResult([], value);
    }
}