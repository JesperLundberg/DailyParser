﻿using DailyParser.DataAccess.DatabaseContexts;
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
        return await DayContext.ParsedDays.ToListAsync();
    }

    public async Task<ParsedDay?> GetDayAsync(Guid gameId)
    {
        return await DayContext.ParsedDays.FindAsync(gameId);
    }

    public async Task<IEnumerable<ParsedDay>> GetDaysByDateRangeAsync(
        DateTime fromDate,
        DateTime toDate
    )
    {
        return await DayContext.ParsedDays
            .Where(game => game.Date > fromDate && game.Date < toDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<ParsedDay>> GetDaysByFromDateAsync(DateTime fromDate)
    {
        return await DayContext.ParsedDays.Where(game => game.Date > fromDate).ToListAsync();
    }

    public async Task<bool> CreateParsedDayAsync(IEnumerable<ParsedText> fileModelToSave)
    {
        var fileModels = fileModelToSave.Select(
            fileModel =>
                new ParsedDay
                {
                    Id = default,
                    Date = DateTime.TryParse(fileModel.Name, out var date) ? date : default,
                    Games = fileModel.Texts
                        .Select(x => new Game { Id = default, Name = x })
                        .ToList()
                }
        );

        await DayContext.ParsedDays.AddRangeAsync(fileModels);
        return await DayContext.SaveChangesAsync() != 0;
    }
}
