namespace DailyParser.DataAccess.Models;

public record FileNameAndPath
{
    public string Name { get; init; } = null!;
    public string FullPath { get; init; } = null!;
}
