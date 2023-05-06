namespace DailyParser.DataAccess.Models;

public class ParsedDay
{
    public Guid Id { get; set; }
    public DateTime Date { get; init; }
    public List<Game> Games { get; init; } = new();
}
