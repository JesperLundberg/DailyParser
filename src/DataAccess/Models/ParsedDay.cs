namespace DailyParser.DataAccess.Models;

public class ParsedDay
{
    public Guid Id { get; set; }
    public DateTime Date { get; init; }
    public string Category { get; init; } = string.Empty;
    public List<Game> Games { get; set; } = new();
    public List<Excercise> Excercises { get; set; } = new();
}
