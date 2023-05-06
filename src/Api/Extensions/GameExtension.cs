using DailyParser.Api.Dtos;
using DailyParser.DataAccess.Models;

namespace DailyParser.Api.Extensions;

public static class GameExtension
{
  public static GameDto ToDto(this Game game)
  {
    return new GameDto
    {
      Name = game.Name,
    };
  }

  public static IEnumerable<GameDto> ToDto(this IEnumerable<Game> games)
  {
    return games.Select(x => x.ToDto());
  }
}
