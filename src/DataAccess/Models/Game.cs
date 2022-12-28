namespace DataAccess.Models;

public class Game
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateOnly Started { get; set; }
    public DateOnly Finished { get; set; }
}