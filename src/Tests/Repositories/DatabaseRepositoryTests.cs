using DailyParser.DataAccess.Repositories;
using DailyParser.Tests.Factories;

namespace DailyParser.Tests.Repositories;

public class DatabaseRepositoryTests
{
    private IDatabaseRepository DatabaseRepository { get; set; } = null!;

    [SetUp]
    public void Setup()
    {
        // Setup is run before each test so the database is recreated each time
        var gameContext = DatabaseContextFactory.Create();
        DatabaseRepository = new DatabaseRepository(gameContext);
    }

    [Test]
    public async Task SaveFilesWithContentInDatabaseAsync_WithValidFileContent_SavesInDatabase()
    {
        // Arrange
        var fileContent = FileContentFactory.CreateValidFileContents(1);
        var parsedText = ParsedTextFactory.CreateValidParsedTexts(1);

        // Act
        await DatabaseRepository.SaveFilesWithContentInDatabaseAsync(parsedText);

        // Assert
    }
}
