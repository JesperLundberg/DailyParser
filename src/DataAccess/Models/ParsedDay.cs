namespace DailyParser.DataAccess.Models;

public class ParsedDay
{
    public Guid Id { get; set; }
    public DateTime Date { get; init; }
    public List<SubCategories> ParsedSubCategories { get; set; } = new();
    public List<Game> Games { get; set; } = new();
}
