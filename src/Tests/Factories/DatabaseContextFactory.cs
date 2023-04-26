using DailyParser.DataAccess.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace DailyParser.Tests.Factories;

public static class DatabaseContextFactory
{
    public static GameContext Create()
    {
        var options = new DbContextOptionsBuilder<GameContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new GameContext(options);
    }
}
