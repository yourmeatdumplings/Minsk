using Minsk.CodeAnalysis;
using Minsk.CodeAnalysis.Binding;
using Minsk.CodeAnalysis.Syntax;

// ReSharper disable once CheckNamespace
namespace Minsk;

internal static class Program
{
    private static void Main()
    {
        var showTree = false;
        while (true)
        {
            Console.Write("> ");
            var line = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line)) return;

            switch (line)
            {
                case "#showTree":
                    showTree = !showTree;
                    Console.WriteLine(showTree ? "Showing parse trees." : "Not showing parse trees.");
                    continue;
                case "#cls":
                    Console.Clear();
                    continue;
            }
                
            var syntaxTree = SyntaxTree.Parse(line);
            var compilation = new Compilation(syntaxTree);
            var result = compilation.Evaluate();
                
            var diagnostics = result.Diagnostics;

            if (showTree)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                PrettyPrint(syntaxTree.Root);
                Console.ResetColor();
            }

            if (!diagnostics.Any())
            {
                Console.WriteLine(result.Value);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;

                foreach (var diagnostic in diagnostics)
                    Console.WriteLine(diagnostic);

                Console.ResetColor();
            }
        }
    }

    static void PrettyPrint(SyntaxNode node, string indent = "", bool isLast = true)
    {
        var marker = isLast ? "\u2514\u2500\u2500" : "\u251c\u2500\u2500";
            
        Console.Write(indent);
        Console.Write(marker);
        Console.Write(node.Kind);

        if (node is SyntaxToken { Value: not null } t)
        {
            Console.Write(" ");
            Console.Write(t.Value);
        }
            
        Console.WriteLine();

        // indent += "    ";

        indent += isLast ? "   " : "\u2502  ";

        var lastChild = node.GetChildren().LastOrDefault();

        foreach (var child in node.GetChildren())
        {
            PrettyPrint(child, indent, child == lastChild);
        }
    }
}