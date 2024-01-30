namespace DailyParser.DataAccess.Models;

public class Excercise
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public ParsedDay ParsedDay { get; init; } = default!;
};
