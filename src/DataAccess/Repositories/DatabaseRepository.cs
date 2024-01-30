using DailyParser.DataAccess.DatabaseContexts;
using DailyParser.DataAccess.Extensions;
using DailyParser.DataAccess.Models;
using DailyParser.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyParser.DataAccess.Repositories;

public class DatabaseRepository : IDatabaseRepository, IDisposable
{
    private DayContext DayContext { get; }

    public DatabaseRepository(DayContext dayContext)
    {
        DayContext = dayContext;
    }

    public async Task<IEnumerable<ParsedDay>> GetAllDaysAsync()
    {
        return await DayContext.ParsedDays.Include(p => p.Games).ToListAsync();
    }

    public async Task<ParsedDay?> GetDayAsync(Guid dayId)
    {
        return await DayContext
            .ParsedDays.Include(p => p.Games)
            .Where(d => d.Id == dayId)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ParsedDay>> GetDaysByDateRangeAsync(
        DateTime fromDate,
        DateTime toDate
    )
    {
        return await DayContext
            .ParsedDays.Include(p => p.Games)
            .Where(game =>
                game.Date.GetOnlyDate() >= fromDate.GetOnlyDate()
                && game.Date.GetOnlyDate() <= toDate.GetOnlyDate()
            )
            .ToListAsync();
    }

    public async Task<IEnumerable<ParsedDay>> GetDaysByFromDateAsync(DateTime fromDate)
    {
        return await DayContext
            .ParsedDays.Include(p => p.Games)
            .Where(game => game.Date.GetOnlyDate() >= fromDate.GetOnlyDate())
            .ToListAsync();
    }

    public async Task<bool> CreateParsedDayAsync(IEnumerable<ParsedText> fileModelToSave)
    {
        // Remove all days that do not parse into a valid date
        fileModelToSave = fileModelToSave.Where(fileModel =>
            DateTime.TryParse(fileModel.Name, out var date) && date.GetOnlyDate() != default
        );

        // TODO: Move this out to the caller, this method should not be responsible for this
        var parsedDaysToSave = fileModelToSave.Select(fileModel => new ParsedDay
        {
            Id = default,
            Date = DateTime.Parse(fileModel.Name),
            Category = fileModel.Category,
            Games = fileModel.Texts.Select(x => new Game { Id = default, Name = x }).ToList()
        });

        var parsedDayTasks = new List<Task>();

        foreach (var parsedDay in parsedDaysToSave)
        {
            parsedDayTasks.Add(AddOrUpdateAsync(parsedDay));
        }

        await Task.WhenAll(parsedDayTasks);

        var saveResult = await DayContext.SaveChangesAsync();

        return saveResult > 0;
    }

    private async Task AddOrUpdateAsync(ParsedDay parsedDay)
    {
        var exists = DayContext
            .ParsedDays.Include(x => x.Games)
            .FirstOrDefault(day => day.Date == parsedDay.Date);

        if (exists != null)
        {
            // TODO: Can this be rewritten in a better way?
            // Add games that does not exist in the database
            foreach (var game in parsedDay.Games)
            {
                var gameExists = exists.Games.FirstOrDefault(g => g.Name == game.Name);

                if (gameExists == null)
                {
                    exists.Games.Add(game);
                }
            }
        }
        else
        {
            await DayContext.ParsedDays.AddAsync(parsedDay);
        }
    }

    private bool disposed = false;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                DayContext.Dispose();
            }

            disposed = true;
        }
    }

    ~DatabaseRepository()
    {
        Dispose(false);
    }
}
