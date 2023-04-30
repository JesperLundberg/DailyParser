namespace DailyParser.DataAccess.Models;

public record Game
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
