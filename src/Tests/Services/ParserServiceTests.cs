using DailyParser.DataAccess.Repositories;
using DailyParser.Parser.Constants;
using DailyParser.Parser.Services;
using DailyParser.Tests.Factories;

namespace DailyParser.Tests.Services;

public class ParserServiceTests
{
    private IDatabaseRepository DatabaseRepository { get; set; } = null!;
    private IFileSystemRepository FileSystemRepository { get; set; } = null!;
    private IParserService ParserService { get; set; } = null!;

    [SetUp]
    public void Setup()
    {
        // Setup is run before each test so the database is recreated each time
        var gameContext = DatabaseContextFactory.Create();
        DatabaseRepository = new DatabaseRepository(gameContext);

        ParserService = new ParserService(DatabaseRepository, FileSystemRepository);
    }

    [Test]
    public async Task ParseTextAsync_WithContentNotMatchingSearchedForText_ReturnsAListOfAllFilesWithEmptyTexts()
    {
        // Arrange
        var parserService = new ParserService(DatabaseRepository, FileSystemRepository);
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
        var fileContent = FileContentFactory.CreateValidFileContents(3);

        // Act
        var result = await ParserService.ParseTextAsync(fileContent, RegEx.Game);

        // Assert
        Assert.That(result.Count, Is.EqualTo(fileContent.Count()));
        Assert.That(result.First().Texts.First(), Is.EqualTo("Primordia"));
        Assert.That(result.First().Texts.Last(), Is.EqualTo("Outcast"));
    }

    [Test]
    public async Task ParseTextAsync_WithMixedContentMatchingSearchedForText_ReturnsAListOfAllFilesWithMatchedTextAsTexts()
    {
        // Arrange
        var fileContent = FileContentFactory.CreateValidFileContents(1).ToList();
        fileContent.AddRange(FileContentFactory.CreateInvalidFileContents(1));

        // Act
        var result = await ParserService.ParseTextAsync(fileContent, RegEx.Game);

        // Assert
        Assert.That(result.Count, Is.EqualTo(fileContent.Count));
        Assert.That(result.First().Texts.First(), Is.EqualTo("Primordia"));
        Assert.That(result.First().Texts.Last(), Is.EqualTo("Outcast"));
        Assert.That(result.Last().Texts, Is.Empty);
    }
}
