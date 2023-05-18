using DailyParser.DataAccess.Wrappers;

namespace DailyParser.Tests.Fakes;

public class DirectoryFake : IDirectory
{
    private static int folderCount;

    public IEnumerable<string> EnumerateDirectories(string path)
    {
        var directoriesToReturn = folderCount switch
        {
            0 => new[] { "folder1", "folder2", "folder3" },
            1 => new[] { "folder4" },
            _ => Enumerable.Empty<string>()
        };

        folderCount += 1;

        return directoriesToReturn;
    }

    public IEnumerable<string> EnumerateFiles(string path)
    {
        var randomDate = DateTime.Now.AddDays(new Random().Next(-500, -100));
        var filesToReturn = folderCount switch
        {
            1
                => new[]
                {
                    $"/folder1/{DateTime.Now.AddDays(-1):yyyy-MM-dd}.md",
                    $"/folder1/{DateTime.Now.AddDays(new Random().Next(-500, -100)):yyyy-MM-dd}.md",
                    $"/folder1/{DateTime.Now.AddDays(new Random().Next(-500, -100)):yyyy-MM-dd}.md"
                },
            2 => new[] { $"/folder2/{DateTime.Now.AddDays(new Random().Next(-500, -100)):yyyy-MM-dd}.md" },
            3
                => new[]
                {
                    $"/folder3/{DateTime.Now.AddDays(new Random().Next(-500, -100)):yyyy-MM-dd}.md",
                    $"/folder3/{DateTime.Now.AddDays(new Random().Next(-500, -100)):yyyy-MM-dd}.md"
                },
            4
                => new[]
                {
                    $"/folder4/{DateTime.Now.AddDays(new Random().Next(-500, -100)):yyyy-MM-dd}.md",
                    $"/folder4/{DateTime.Now.AddDays(new Random().Next(-500, -100)):yyyy-MM-dd}.md"
                },
            _ => Enumerable.Empty<string>()
        };

        return filesToReturn;
    }
}
