using DailyParser.DataAccess.DatabaseContexts;
using DailyParser.DataAccess.Extensions;
using DailyParser.DataAccess.Models;
using DailyParser.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyParser.DataAccess.Repositories;

public class DatabaseRepository : IDatabaseRepository
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
        return await DayContext.ParsedDays
            .Include(p => p.Games)
            .Where(d => d.Id == dayId)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ParsedDay>> GetDaysByDateRangeAsync(
        DateTime fromDate,
        DateTime toDate
    )
    {
        return await DayContext.ParsedDays
            .Include(p => p.Games)
            .Where(
                game =>
                    game.Date.GetOnlyDate() >= fromDate.GetOnlyDate()
                    && game.Date.GetOnlyDate() <= toDate.GetOnlyDate()
            )
            .ToListAsync();
    }

    public async Task<IEnumerable<ParsedDay>> GetDaysByFromDateAsync(DateTime fromDate)
    {
        return await DayContext.ParsedDays
            .Include(p => p.Games)
            .Where(game => game.Date.GetOnlyDate() >= fromDate.GetOnlyDate())
            .ToListAsync();
    }

    public async Task<bool> CreateParsedDayAsync(IEnumerable<ParsedText> fileModelToSave)
    {
        var fileModels = fileModelToSave.Select(
            fileModel =>
                new ParsedDay
                {
                    Id = default,
                    Date = DateTime.TryParse(fileModel.Name, out var date)
                        ? date.GetOnlyDate()
                        : default,
                    Games = fileModel.Texts
                        .Select(x => new Game { Id = default, Name = x })
                        .ToList()
                }
        );

        // Find all days in database that are also in fileModelToSave using contains and not Any
        var daysInDatabase = DayContext.ParsedDays
            .Where(day => fileModels.Contains(day))
            .ToList();
        // var daysInDatabase = DayContext.ParsedDays
        //     .Where(day => fileModels.Any(fileModel => fileModel.Date == day.Date))
        //     .ToList();
        
        // Update all days in filemodels with the id from the database if it exists
        foreach (var day in daysInDatabase)
        {
            var dayInFileModels = fileModels.FirstOrDefault(fileModel => fileModel.Date == day.Date);
            if (dayInFileModels != null)
            {
                dayInFileModels.Id = day.Id;
            }
        }

        await DayContext.ParsedDays.AddRangeAsync(fileModels);
        return await DayContext.SaveChangesAsync() != 0;
    }
}
