namespace DailyParser.DataAccess.Models;

public record ParsedDay
{
    public Guid Id { get; set; }
    public DateTime Date { get; init; }
    public IEnumerable<Game> Games { get; init; } = Enumerable.Empty<Game>();
}
