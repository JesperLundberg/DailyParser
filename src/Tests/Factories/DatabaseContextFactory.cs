using DailyParser.DataAccess.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace DailyParser.Tests.Factories;

public static class DatabaseContextFactory
{
    public static DayContext Create()
    {
        var options = new DbContextOptionsBuilder<DayContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new DayContext(options);
    }
}
