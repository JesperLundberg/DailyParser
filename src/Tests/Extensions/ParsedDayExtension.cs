using DailyParser.Api.Extensions;
using DailyParser.DataAccess.Models;

namespace DailyParser.Tests.Extensions;

public class ParsedDayExtensionTests
{
    [Test]
    public void ToDto_WithEmptyData_ReturnsEmptyDto()
    {
        var parsedDay = new ParsedDay();

        var dto = parsedDay.ToDto();

        Assert.That(dto, Is.Not.Null);
        Assert.That(dto.Date, Is.EqualTo(DateTime.MinValue.ToShortDateString()));
        Assert.That(dto.Games, Is.Empty);
    }

    [Test]
    public void ToDto_WithDataButNoGameData_ReturnsDtoWithEmptyGames()
    {
        var parsedDay = new ParsedDay { Date = DateTime.Now, Games = new List<Game>() };

        var dto = parsedDay.ToDto();

        Assert.That(dto, Is.Not.Null);
        Assert.That(dto.Date, Is.EqualTo(parsedDay.Date.ToShortDateString()));
        Assert.That(dto.Games, Is.Empty);
    }

    [Test]
    public void ToDto_WithDataAndMultipleGames_ReturnsDtoWithFullData()
    {
        var parsedDay = new ParsedDay
        {
            Date = DateTime.Now,
            Games = new List<Game>
            {
                new Game { Name = "Outcast" },
                new Game { Name = "Primordia" }
            }
        };

        var dto = parsedDay.ToDto();

        Assert.That(dto, Is.Not.Null);
        Assert.That(dto.Date, Is.EqualTo(parsedDay.Date.ToShortDateString()));
        Assert.That(dto.Games, Is.Not.Empty);
        Assert.That(dto.Games.Count, Is.EqualTo(2));
        Assert.That(dto.Games.First().Name, Is.EqualTo("Outcast"));
        Assert.That(dto.Games.Last().Name, Is.EqualTo("Primordia"));
    }
}
