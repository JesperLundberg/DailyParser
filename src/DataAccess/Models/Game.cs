namespace DailyParser.DataAccess.Models;

public class Game
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime Started { get; set; }
    public DateTime Finished { get; set; }
}
