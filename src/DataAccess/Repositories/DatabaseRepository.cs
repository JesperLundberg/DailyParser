using DailyParser.DataAccess.DatabaseContexts;
using DailyParser.DataAccess.Models;
using DailyParser.Models.Models;
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
            .Where(game => game.Date > fromDate && game.Date < toDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Game>> GetGamesByFromDateAsync(DateTime fromDate)
    {
        return await GameContext.Games.Where(game => game.Date > fromDate).ToListAsync();
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

    public Task<bool> SaveFilesWithContentInDatabaseAsync(IEnumerable<ParsedText> fileModelToSave)
    {
        // TODO: Make this an extension method instead
        // var fileModels = fileModelToSave.Select(
        //     fileModel =>
        //         new Game(
        //             Id: default,
        //             fileModel.Name,
        //             DateTime.TryParse(fileModel.FileName, out var date) ? date : default
        //         )
        // );

        // TODO: Save Games in Database
        // await GameContext.Games.AddRangeAsync(fileModels);
        // return await GameContext.SaveChangesAsync() != 0;
        return Task.FromResult(true);
    }

}
