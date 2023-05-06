namespace DailyParser.Api.Dtos;

public record ParsedDayDto
{
    public Guid Id { get; init; }
    public DateTime Date { get; init; }
    public IEnumerable<GameDto> Games { get; set; } = Enumerable.Empty<GameDto>();
}
