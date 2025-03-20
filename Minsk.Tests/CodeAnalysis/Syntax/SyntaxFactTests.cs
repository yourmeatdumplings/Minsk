using Minsk.CodeAnalysis.Syntax;

namespace Minsk.Tests.CodeAnalysis.Syntax;

public class SyntaxFactTests
{
    [Theory, MemberData(nameof(GetSyntaxKindData))]
    public void SyntaxFact_GetText_RoundTrips(SyntaxKind kind)
    {
        var text = SyntaxFacts.GetText(kind);
        if (text == null) return;

        var tokens = SyntaxTree.ParseTokens(text);
        var token = Assert.Single(tokens);
        Assert.Equal(kind, token.Kind);
        Assert.Equal(text, token.Text);
    }

    public static IEnumerable<object[]> GetSyntaxKindData()
    {
        var kinds = Enum.GetValues<SyntaxKind>();
        foreach (var kind in kinds)
            yield return [kind];
    }
}