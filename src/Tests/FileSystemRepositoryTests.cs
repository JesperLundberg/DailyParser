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
        var filesystemRepository = new FileSystemRepository(new DirectoryFake());

        var result = await filesystemRepository.GetFileListAsync("pathdoesnotmatterintest/");

        Assert.AreEqual(8, result.Count());
    }

    [Test]
    public void PassTest()
    {
      Assert.Pass();
    }
}
