using DailyParser.DataAccess.Models;
using DailyParser.Models.Models;

namespace DailyParser.DataAccess.Repositories;

public interface IDatabaseRepository
{
    Task<IEnumerable<ParsedDay>> GetAllDaysAsync();

    Task<ParsedDay?> GetDayAsync(Guid gameId);

    Task<IEnumerable<ParsedDay>> GetDaysByDateRangeAsync(DateTime fromDate, DateTime toDate);

    Task<IEnumerable<ParsedDay>> GetDaysByFromDateAsync(DateTime fromDate);

    Task<bool> CreateParsedDayAsync(IEnumerable<ParsedText> fileModelToSave);
}
