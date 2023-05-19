using DailyParser.DataAccess.Extensions;
using DailyParser.DataAccess.Models;
using DailyParser.DataAccess.Repositories;
using DailyParser.Parser.Constants;
using DailyParser.Parser.Services;
using DailyParser.Tests.Factories;
using DailyParser.Tests.Fakes;

namespace DailyParser.Tests.Services;

[SingleThreaded]
public class ParserServiceTests
{
    [SetUp]
    public void Setup() { }

    [Test]
    public async Task ParseTextAsync_WithContentNotMatchingSearchedForText_ReturnsAListOfAllFilesWithEmptyTexts()
    {
        // Arrange
        var dayContext = DatabaseContextFactory.Create(Guid.NewGuid().ToString());
        var databaseRepository = new DatabaseRepository(dayContext);
        var fileSystemRepository = new FileSystemRepository(
            new DirectoryFake(),
            new FileReaderFake()
        );

        var parserService = new ParserService(databaseRepository, fileSystemRepository);

        // Arrange
        var fileContent = FileContentFactory.CreateInvalidFileContents(3);

        // Act
        var result = await parserService.ParseTextAsync(fileContent, RegEx.Game);

        // Assert
        Assert.That(result.Count, Is.EqualTo(3));
        CollectionAssert.AllItemsAreNotNull(result);
        CollectionAssert.IsEmpty(result.First().Texts);
    }

    [Test]
    public async Task ParseTextAsync_WithContentMatchingSearchedForText_ReturnsAListOfAllFilesWithMatchedTextAsTexts()
    {
        // Arrange
        var dayContext = DatabaseContextFactory.Create(Guid.NewGuid().ToString());
        var databaseRepository = new DatabaseRepository(dayContext);
        var fileSystemRepository = new FileSystemRepository(
            new DirectoryFake(),
            new FileReaderFake()
        );

        var parserService = new ParserService(databaseRepository, fileSystemRepository);

        // Arrange
        var fileContent = FileContentFactory.CreateValidFileContents(3);

        // Act
        var result = await parserService.ParseTextAsync(fileContent, RegEx.Game);

        // Assert
        Assert.That(result.Count, Is.EqualTo(fileContent.Count()));
        Assert.That(result.First().Texts.First(), Is.EqualTo("Primordia"));
        Assert.That(result.First().Texts.Last(), Is.EqualTo("Outcast"));
    }

    [Test]
    public async Task ParseTextAsync_WithMixedContentMatchingSearchedForText_ReturnsAListOfAllFilesWithMatchedTextAsTexts()
    {
        // Arrange
        var dayContext = DatabaseContextFactory.Create(Guid.NewGuid().ToString());
        var databaseRepository = new DatabaseRepository(dayContext);
        var fileSystemRepository = new FileSystemRepository(
            new DirectoryFake(),
            new FileReaderFake()
        );

        var parserService = new ParserService(databaseRepository, fileSystemRepository);

        // Arrange
        var fileContent = FileContentFactory.CreateValidFileContents(1).ToList();
        fileContent.AddRange(FileContentFactory.CreateInvalidFileContents(1));

        // Act
        var result = await parserService.ParseTextAsync(fileContent, RegEx.Game);

        // Assert
        Assert.That(result.Count, Is.EqualTo(fileContent.Count));
        Assert.That(result.First().Texts.First(), Is.EqualTo("Primordia"));
        Assert.That(result.First().Texts.Last(), Is.EqualTo("Outcast"));
        Assert.That(result.Last().Texts, Is.Empty);
    }

    [Test]
    public async Task ParseTextAsync_WithDateOnlyName_CorrectlyStripsPathAndExtensionFromName()
    {
        // Arrange
        var dayContext = DatabaseContextFactory.Create(Guid.NewGuid().ToString());
        var databaseRepository = new DatabaseRepository(dayContext);
        var fileSystemRepository = new FileSystemRepository(
            new DirectoryFake(),
            new FileReaderFake()
        );

        var parserService = new ParserService(databaseRepository, fileSystemRepository);

        // Arrange
        var fileContent = new FileContent
        {
            FileName = $"{DateTime.Now:yyyy-MM-dd}",
            Content =
                @$"This is not a valid
                  file content {Guid.NewGuid().ToString()}"
        };

        // Act
        var result = await parserService.ParseTextAsync(
            new List<FileContent> { fileContent },
            RegEx.Game
        );

        // Assert
        Assert.That(result.First().Name, Is.EqualTo($"{DateTime.Now:yyyy-MM-dd}"));
    }

    [Test]
    public async Task ParseIntoDb_WithValidFileContents_SavesFilesIntoDatabase()
    {
        // Arrange
        var dayContext = DatabaseContextFactory.Create(Guid.NewGuid().ToString());
        var databaseRepository = new DatabaseRepository(dayContext);
        var fileSystemRepository = new FileSystemRepository(
            new DirectoryFake(),
            new FileReaderFake()
        );

        var parserService = new ParserService(databaseRepository, fileSystemRepository);

        // Act
        var parseReturn = await parserService.ParseIntoDbAsync("pathDoesNotMatter");

        // Assert
        Assert.That(parseReturn, Is.True);

        var result = await databaseRepository.GetAllDaysAsync();
        Assert.That(result.Count(), Is.EqualTo(8));
    }

    [Test]
    public async Task ParseIntoDb_WithExistingDayInDatabase_UpdatesExistingDayAndCreatesRest()
    {
        // Arrange
        var dayContext = DatabaseContextFactory.Create(Guid.NewGuid().ToString());
        var databaseRepository = new DatabaseRepository(dayContext);
        var fileSystemRepository = new FileSystemRepository(
            new DirectoryFake(),
            new FileReaderFake()
        );

        var parserService = new ParserService(databaseRepository, fileSystemRepository);

        // Arrange
        var existingDay = new ParsedDay
        {
            Date = DateTime.Now.AddDays(-1).GetOnlyDate(),
            Games = new List<Game>
            {
                new Game { Name = "GameThatDoesNotExist" },
                new Game { Name = "GameThatDoesNotExist2" }
            }
        };

        await dayContext.ParsedDays.AddAsync(existingDay);
        await dayContext.SaveChangesAsync();

        // Act
        var parseReturn = await parserService.ParseIntoDbAsync("pathDoesNotMatter");

        // Assert
        Assert.That(parseReturn, Is.True);

        var result = dayContext.ParsedDays.ToList();
        Assert.That(result.Count(), Is.EqualTo(8));
        Assert.That(result.First().Games.Count, Is.EqualTo(0));
    }
}
