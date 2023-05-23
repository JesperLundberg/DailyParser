using DailyParser.Api.Dtos;
using DailyParser.DataAccess.Models;

namespace DailyParser.Api.Extensions;

public static class ParsedDayExtension
{
  public static ParsedDayDto ToDto(this ParsedDay parsedDay)
  {
    return new ParsedDayDto
    {
      Id = parsedDay.Id,
      Date = parsedDay.Date.ToString("yyyy-MM-dd"),
      Games = parsedDay.Games.ToDto(),
    };
  }

  public static IEnumerable<ParsedDayDto> ToDto(this IEnumerable<ParsedDay> parsedDays)
  {
    return parsedDays.Select(x => x.ToDto());
  }
}
