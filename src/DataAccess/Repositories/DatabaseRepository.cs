using DailyParser.DataAccess.DatabaseContexts;
using DailyParser.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyParser.DataAccess.Repositories;

public class DatabaseRepository : IDatabaseRepository
{
    private GameContext GameContext { get; }

    public DatabaseRepository(GameContext gameContext)
    {
        GameContext = gameContext;
    }

    public async Task<IEnumerable<Game>> GetAllGamesAsync()
    {
        return await GameContext.Games.ToListAsync();
    }

    public async Task<Game?> GetGameAsync(Guid gameId)
    {
        return await GameContext.Games.FindAsync(gameId);
    }

    public async Task<IEnumerable<Game>> GetGamesByDateRangeAsync(
        DateTime fromDate,
        DateTime toDate
    )
    {
        return await GameContext.Games
            .Where(game => game.Started > fromDate && game.Finished < toDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Game>> GetGamesByFromDateAsync(DateTime fromDate)
    {
        return await GameContext.Games.Where(game => game.Started > fromDate).ToListAsync();
    }

    public async Task<bool> CreateGameAsync(Game game)
    {
        await GameContext.AddAsync(game);

        return await GameContext.SaveChangesAsync() != 0;
    }

    public async Task<bool> CreateGamesAsync(IEnumerable<Game> games)
    {
        await GameContext.Games.AddRangeAsync(games);

        return await GameContext.SaveChangesAsync() != 0;
    }

    public async Task<bool> SaveFilesWithContentInDatabaseAsync<T>(IEnumerable<> fileModelToSave)
        where T : Game
    {
        // TODO: Make this general. Right now T does not matter as it's always of Game type
        // TODO: Make this an extension method instead
        var fileModels = fileModelToSave.Select(
            fileModel =>
                new Game
                {
                    Title = fileModel.Title,
                    Started = fileModel.Started,
                    Finished = fileModel.Finished,
                }
        );

        // TODO: Save Games in Database
        await GameContext.Games.AddRangeAsync(fileModels);
        return await GameContext.SaveChangesAsync() != 0;
    }
}
