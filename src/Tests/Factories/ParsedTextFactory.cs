using DailyParser.Models.Models;

namespace DailyParser.Tests.Factories;

public static class ParsedTextFactory
{
    public static IEnumerable<ParsedText> CreateValidParsedTexts(
        int howManyToCreate
    )
    {
        for (var i = 0; i < howManyToCreate; i++)
        {
            yield return new ParsedText{
                Name = Guid.NewGuid().ToString(),
                Texts = new List<string> { "Primordia", "Outcast" }
            };
        }
    }
}
