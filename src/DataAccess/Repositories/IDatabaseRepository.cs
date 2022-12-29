using DataAccess.Models;

namespace DataAccess.Repositories;

public interface IDatabaseRepository
{
    Task<IEnumerable<Game>> GetAllGamesAsync();

    Task<Game?> GetGameAsync(Guid gameId);
}