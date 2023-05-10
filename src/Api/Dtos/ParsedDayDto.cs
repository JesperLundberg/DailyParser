namespace DailyParser.Api.Dtos;

public record ParsedDayDto
{
    public Guid Id { get; init; }
    public string Date { get; init; } = string.Empty;
    public IEnumerable<GameDto> Games { get; set; } = Enumerable.Empty<GameDto>();
}
