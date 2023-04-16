using DailyParser.DataAccess.Models;

namespace DailyParser.DataAccess.Repositories;

public interface IDatabaseRepository
{
    Task<IEnumerable<Game>> GetAllGamesAsync();

    Task<Game?> GetGameAsync(Guid gameId);

    Task<IEnumerable<Game>> GetGamesByDateRangeAsync(DateOnly fromDate, DateOnly toDate);
    
    Task<IEnumerable<Game>> GetGamesByFromDateAsync(DateOnly fromDate);

    Task<bool> CreateGameAsync(Game game);

    Task<bool> CreateGamesAsync(IEnumerable<Game> games);
}
