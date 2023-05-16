using DailyParser.Parser.Services;
using Microsoft.AspNetCore.Mvc;

namespace DailyParser.Api.Controllers;

[ApiController]
[Route("api/parser")]
public class ParserController : ControllerBase
{
    private IParserService ParserService { get; set; }
    private IConfiguration Configuration { get; set; }

    public ParserController(IParserService parserService, IConfiguration configuration)
    {
        ParserService = parserService;
        Configuration = configuration;
    }

    [HttpGet]
    public async Task<ActionResult<bool>> TriggerParseAsync()
    {
        var path = Configuration.GetValue<string>("PathToFiles");
        Console.WriteLine($"Path to files: {path}");

        var result = await ParserService.ParseIntoDbAsync(path!);

        return Ok(result);
    }
}
