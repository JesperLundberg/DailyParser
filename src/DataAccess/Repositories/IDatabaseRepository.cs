using DailyParser.DataAccess.Models;
using DailyParser.Models.Models;

namespace DailyParser.DataAccess.Repositories;

public interface IDatabaseRepository
{
    Task<IEnumerable<Game>> GetAllGamesAsync();

    Task<Game?> GetGameAsync(Guid gameId);

    Task<IEnumerable<Game>> GetGamesByDateRangeAsync(DateTime fromDate, DateTime toDate);

    Task<IEnumerable<Game>> GetGamesByFromDateAsync(DateTime fromDate);

    Task<bool> CreateGameAsync(Game game);

    Task<bool> CreateGamesAsync(IEnumerable<Game> games);

    Task<bool> SaveFilesWithContentInDatabaseAsync(
        IEnumerable<ParsedText> fileModelToSave
    );
}
