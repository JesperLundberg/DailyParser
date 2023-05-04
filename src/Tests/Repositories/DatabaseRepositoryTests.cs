using DailyParser.DataAccess.DatabaseContexts;
using DailyParser.DataAccess.Models;
using DailyParser.DataAccess.Repositories;
using DailyParser.Tests.Factories;

namespace DailyParser.Tests.Repositories;

public class DatabaseRepositoryTests
{
    private IDatabaseRepository DatabaseRepository { get; set; } = null!;
    public DayContext DayContext { get; set; } = null!;

    [SetUp]
    public void Setup()
    {
        // Setup is run before each test so the database is recreated each time
        DayContext = DatabaseContextFactory.Create();
        DatabaseRepository = new DatabaseRepository(DayContext);
    }

    [Test]
    public async Task SaveFilesWithContentInDatabaseAsync_WithValidFileContent_SavesInDatabase()
    {
        // Arrange
        var fileContent = FileContentFactory.CreateValidFileContents(1);
        var parsedText = ParsedTextFactory.CreateValidParsedTexts(1);

        // Act
        var result = await DatabaseRepository.CreateParsedDayAsync(parsedText);

        // Assert
        Assert.That(result, Is.True);
        Assert.That(DayContext.ParsedDays.Count(), Is.EqualTo(1));
        Assert.That(DayContext.ParsedDays.First().Games.First().Name, Is.EqualTo("Primordia"));
        Assert.That(DayContext.ParsedDays.Last().Games.Last().Name, Is.EqualTo("Outcast"));
    }

    [Test]
    public async Task GetAllDaysAsync_WithDataInDatabase_ReturnsAllDaysInDatabase()
    {
        // Arrange
        var day = new ParsedDay
        {
            Date = DateTime.Now.AddDays(-1),
            Games = new List<Game> { new Game { Name = "Primordia" } }
        };

        DayContext.ParsedDays.Add(day);
        await DayContext.SaveChangesAsync();

        // Act
        var result = await DatabaseRepository.GetAllDaysAsync();

        // Assert
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First(), Is.EqualTo(day));
    }

    [Test]
    public async Task GetAllDaysAsync_WithNoDataInDatabase_ReturnsEmptyList()
    {
        // Arrange

        // Act
        var result = await DatabaseRepository.GetAllDaysAsync();

        // Assert
        Assert.That(result.Count(), Is.EqualTo(0));
    }

    [Test]
    public async Task GetAllDaysAsync_WithGames_ReturnsFullGameInfoWithDay()
    {
        // Arrange
        var day = new ParsedDay
        {
            Date = DateTime.Now.AddDays(-1),
            Games = new List<Game>
            {
                new Game { Name = "Primordia" },
                new Game { Name = "Outcast" }
            }
        };

        DayContext.ParsedDays.Add(day);
        await DayContext.SaveChangesAsync();

        // Act
        var result = await DatabaseRepository.GetAllDaysAsync();

        // Assert
        Assert.That(result.First().Games.First().Name, Is.EqualTo("Primordia"));
        Assert.That(result.First().Games.Last().Name, Is.EqualTo("Outcast"));
    }

    [Test]
    public async Task GetDayAsync_WithValidExisting_ReturnsDay()
    {
        // Arrange
        var day = new ParsedDay
        {
            Date = DateTime.Now.AddDays(-1),
            Games = new List<Game> { new Game { Name = "Primordia" } }
        };

        await DayContext.ParsedDays.AddAsync(day);
        await DayContext.SaveChangesAsync();

        // Act
        var result = await DatabaseRepository.GetDayAsync(day.Id);

        // Assert
        Assert.That(result, Is.EqualTo(day));
    }

    [Test]
    public async Task GetDayAsync_WithNonExistingId_ReturnsNull()
    {
        // Arrange
        var day = new ParsedDay
        {
            Date = DateTime.Now.AddDays(-1),
            Games = new List<Game> { new Game { Name = "Primordia" } }
        };

        await DayContext.ParsedDays.AddAsync(day);
        await DayContext.SaveChangesAsync();

        // Act
        var result = await DatabaseRepository.GetDayAsync(default);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetDaysByDateRangeAsync_WithValidDateRange_ReturnsDaysWithinRange()
    {
        // Arrange
        var days = new List<ParsedDay>
        {
            new ParsedDay
            {
                Date = DateTime.Now.AddDays(-1),
                Games = new List<Game> { new Game { Name = "Primordia" } }
            },
            new ParsedDay
            {
                Date = DateTime.Now.AddDays(-3),
                Games = new List<Game> { new Game { Name = "Outcast" } }
            },
            new ParsedDay
            {
                Date = DateTime.Now.AddDays(3),
                Games = new List<Game> { new Game { Name = "The Surge 2" } }
            },
        };

        await DayContext.ParsedDays.AddRangeAsync(days);
        await DayContext.SaveChangesAsync();

        // Act
        var result = await DatabaseRepository.GetDaysByDateRangeAsync(
            DateTime.Now.AddDays(-2),
            DateTime.Now.AddDays(2)
        );

        // Assert
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First(), Is.EqualTo(days.First()));
    }

    [Test]
    public async Task GetDaysByFromDateAsync_WithValidDate_ReturnsDaysAfterDate()
    {
        // Arrange
        var days = new List<ParsedDay>
        {
            new ParsedDay
            {
                Date = DateTime.Now.AddDays(-1),
                Games = new List<Game> { new Game { Name = "Primordia" } }
            },
            new ParsedDay
            {
                Date = DateTime.Now.AddDays(-3),
                Games = new List<Game> { new Game { Name = "Outcast" } }
            },
            new ParsedDay
            {
                Date = DateTime.Now.AddDays(3),
                Games = new List<Game> { new Game { Name = "The Surge 2" } }
            },
        };

        await DayContext.ParsedDays.AddRangeAsync(days);
        await DayContext.SaveChangesAsync();

        // Act
        var result = await DatabaseRepository.GetDaysByFromDateAsync(DateTime.Now.AddDays(-2));

        // Assert
        Assert.That(result.Count(), Is.EqualTo(2));
        Assert.That(result.First(), Is.EqualTo(days.First()));
        Assert.That(result.Last(), Is.EqualTo(days.Last()));
    }
}
