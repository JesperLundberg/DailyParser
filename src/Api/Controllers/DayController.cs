using DailyParser.Api.Dtos;
using DailyParser.Api.Extensions;
using DailyParser.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DailyParser.Api.Controllers;

[ApiController]
[Route("api/days")]
public class DayController : ControllerBase
{
    public IDatabaseRepository DatabaseRepository { get; set; }

    public DayController(IDatabaseRepository databaseRepository)
    {
        DatabaseRepository = databaseRepository;
    }

    [HttpGet()]
    public async Task<ActionResult<IEnumerable<ParsedDayDto>>> GetAllDays()
    {
        var days = await DatabaseRepository.GetAllDaysAsync();

        return Ok(days.ToDto());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ParsedDayDto>> GetById(Guid id)
    {
        var day = await DatabaseRepository.GetDayAsync(id);

        if (day == null)
        {
            return NotFound();
        }

        return Ok(day.ToDto());
    }

    [HttpGet("fromdate/{date}")]
    public async Task<ActionResult<IEnumerable<ParsedDayDto>>> GetByFromDate(DateTime date)
    {
        var days = await DatabaseRepository.GetDaysByFromDateAsync(date);

        return Ok(days.ToDto());
    }

    [HttpGet("fromdate/{fromDate}/todate/{toDate}")]
    public async Task<ActionResult<IEnumerable<ParsedDayDto>>> GetByDateRange(
        DateTime fromDate,
        DateTime toDate
    )
    {
        var days = await DatabaseRepository.GetDaysByDateRangeAsync(fromDate, toDate);

        return Ok(days.ToDto());
    }
}
