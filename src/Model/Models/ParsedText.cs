namespace DailyParser.Models.Models;

public record ParsedText
{
    public string Name { get; init; } = null!;
    public IEnumerable<string> Texts { get; init; } = null!;
}
