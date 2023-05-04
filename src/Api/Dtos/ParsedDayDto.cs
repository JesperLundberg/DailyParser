namespace DailyParser.Api.Dtos;

public record ParsedDayDto
{
    public Guid Id { get; init; }
    public DateTime Date { get; init; }
}
