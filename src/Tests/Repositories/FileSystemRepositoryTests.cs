using DailyParser.DataAccess.Models;
using DailyParser.DataAccess.Repositories;
using DailyParser.Tests.Fakes;

namespace DailyParser.Tests.Repositories;

public class FileSystemRepositoryTests
{
    private IFileSystemRepository FileSystemRepository { get; set; } = null!;

    [SetUp]
    public void Setup()
    {
        FileSystemRepository = new FileSystemRepository(new DirectoryFake(), new FileReaderFake());
    }

    [Test]
    public async Task GetFileListAsync_ReturnsAllFiles()
    {
        var result = await FileSystemRepository.GetFileListAsync("pathdoesnotmatterintest/");

        Assert.That(
            result.Count() == 8,
            "Number of files should have been 8 but was {0}",
            result.Count()
        );
    }

    [Test]
    public async Task GetFileWithContentAsync_ReturnsFileWithExpectedName()
    {
        var fileNameAndPath = new FileNameAndPath
        {
            Name = "NameOfFile",
            FullPath = "pathdoesnotmatterintest"
        };
        var result = await FileSystemRepository.GetFileWithContentAsync(fileNameAndPath);

        Assert.That(result.FileName, Is.EqualTo("NameOfFile"));
    }

    [Test]
    public async Task GetFilesWithContentAsync_ReturnsFileWithExpectedContent()
    {
        var filesToGet = new List<FileNameAndPath>
        {
            new FileNameAndPath { Name = "NameOfFile", FullPath = "pathdoesnotmatterintest" }
        };

        var result = await FileSystemRepository.GetFilesWithContentAsync(filesToGet);

        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(
            result.First(x => x.FileName == "NameOfFile").Content,
            Is.EqualTo("Game That I Played 1")
        );
    }
}
