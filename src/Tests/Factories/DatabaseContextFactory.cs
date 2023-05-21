using DailyParser.DataAccess.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace DailyParser.Tests.Factories;

public static class DatabaseContextFactory
{
    public static DayContext Create(string name)
    {
        var options = new DbContextOptionsBuilder<DayContext>()
            .UseInMemoryDatabase(name)
            .Options;

        return new DayContext(options);
    }
}
