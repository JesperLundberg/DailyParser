using Tests.Fakes;

namespace DailyParser.Tests;

public class Tests
{
    [SetUp]
    public void Setup() { }

    [Test]
    public void GetDirectories_ReturnsAllDirectories()
    {
        var filesystemRepository = new FileSystemRepositoryFake();

        // This is a useless test as it only tests the fake!
        var result = filesystemRepository.GetDirectories("pathdoesnotmatterintest/");

        Assert.AreEqual(3, result.Count());
    }

    [Test]
    public void GetFilesRecursively_ReturnsAllFiles()
    {
      var filesystemRepository = new FileSystemRepositoryFake();

      var result = filesystemRepository.GetFileListAsync("pathdoesnotmatterintest/");
    }
}
