namespace DailyParser.Api.Dtos;

public record ParsedDayDto
{
    public Guid Id { get; init; }
    public string Date { get; init; } = string.Empty;
    public IEnumerable<string> ParsedSubCategories { get; init; } = Enumerable.Empty<string>();
    public IEnumerable<GameDto> Games { get; init; } = Enumerable.Empty<GameDto>();
}
