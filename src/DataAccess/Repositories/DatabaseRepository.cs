using DataAccess.DatabaseContexts;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

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

    public async Task<IEnumerable<Game>> GetGamesByDateRangeAsync(DateOnly fromDate, DateOnly toDate)
    {
        return await GameContext.Games.Where(game => game.Started > fromDate && game.Finished < toDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Game>> GetGamesByFromDateAsync(DateOnly fromDate)
    {
        return await GameContext.Games.Where(game => game.Started > fromDate)
            .ToListAsync();
    }

    public async Task<bool> CreateGameAsync(Game game)
    {
        await GameContext.AddAsync(game);
        var changedRows = await GameContext.SaveChangesAsync();

        return changedRows != 0;
    }

    public async Task<bool> CreateGamesAsync(IEnumerable<Game> games)
    {
        await GameContext.Games.AddRangeAsync(games);
        var changedRows = await GameContext.SaveChangesAsync();

        return changedRows != 0;
    }
}