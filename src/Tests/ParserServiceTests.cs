using DailyParser.DataAccess.DatabaseContexts;
using DailyParser.DataAccess.Models;
using DailyParser.DataAccess.Repositories;
using DailyParser.Parser.Constants;
using DailyParser.Parser.Services;
using Microsoft.EntityFrameworkCore;

namespace DailyParser.Tests;

public class ParserServiceTests
{
    private IDatabaseRepository DatabaseRepository { get; set; } = null!;
    private IFileSystemRepository FileSystemRepository { get; set; } = null!;

    [SetUp]
    public void Setup()
    {
        // Set up a database context with an inmemory database
        // and a file system repository with DirectoryFake and FileFake
        // to avoid writing to the file system
        var databaseContext = new DatabaseContext(
            new DbContextOptionsBuilder<GameContext>()
                .UseInMemoryDatabase(databaseName: "DailyParser")
                .Options
        );
        DatabaseRepository = new DatabaseRepository(databaseContext);
    }

    [Test]
    public async Task ParseTextAsync_WithValidFileContent_ReturnsAListOfAllTheParsedLines()
    {
        var parserService = new ParserService();
        var fileContent = new List<FileContent>
        {
            new FileContent { FileName = "1", Content = "This is a test" },
            new FileContent { FileName = "2", Content = "This is a test" },
            new FileContent { FileName = "3", Content = "This is a test" }
        };

        // Act
        var result = await parserService.ParseTextAsync(fileContent, RegEx.Game);

        // Assert
        Assert.AreEqual(3, result.Count);
        Assert.AreEqual("1", result[0].Id);
        Assert.AreEqual("2", result[1].Id);
        Assert.AreEqual("3", result[2].Id);
    }
}
