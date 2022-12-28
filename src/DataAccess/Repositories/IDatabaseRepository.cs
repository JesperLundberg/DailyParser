using DataAccess.Models;

namespace DataAccess.Repositories;

public interface IDatabaseRepository
{
    IEnumerable<Game> GetAllGames();
}