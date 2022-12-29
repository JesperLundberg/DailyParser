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
}