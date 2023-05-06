using DailyParser.Api.Dtos;
using DailyParser.Api.Extensions;
using DailyParser.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DailyParser.Api.Controllers;

[ApiController]
[Route("api/day")]
public class DayController : ControllerBase
{
    public IDatabaseRepository DatabaseRepository { get; set; }

    public DayController(IDatabaseRepository databaseRepository)
    {
        DatabaseRepository = databaseRepository;
    }

    [HttpGet("getall")]
    public async Task<ActionResult<IEnumerable<ParsedDayDto>>> GetAllDays()
    {
        var days = await DatabaseRepository.GetAllDaysAsync();

        return Ok(days.ToDto());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ParsedDayDto>> GetById([FromQuery] Guid id)
    {
        var day = await DatabaseRepository.GetDayAsync(id);

        if (day == null)
        {
            return NotFound();
        }

        return Ok(day.ToDto());
    }
}
