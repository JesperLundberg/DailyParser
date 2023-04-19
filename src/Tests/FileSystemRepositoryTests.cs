using DailyParser.DataAccess.Repositories;
using DailyParser.Tests.Fakes;

namespace DailyParser.Tests;

public class FileSystemRepositoryTests
{
    [SetUp]
    public void Setup() { }

    [Test]
    public async Task GetFileListAsync_ReturnsAllFiles()
    {
        var filesystemRepository = new FileSystemRepository(new DirectoryFake(), new FileReaderFake());

        var result = await filesystemRepository.GetFileListAsync("pathdoesnotmatterintest/");

        Assert.That(result.Count() == 8, "Number of files should have been 8 but was {0}", result.Count());
    }

    // [Test]
    // public async Task Get
}
