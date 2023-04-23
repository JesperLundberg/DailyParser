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
        var filesToReturn = folderCount switch
        {
            1 => new[] { "file1.md", "file2.md", "file3.md" },
            2 => new[] { "file4.md" },
            3 => new[] { "file5.md", "file6.md" },
            4 => new[] { "file7.md", "file8.md" },
            _ => Enumerable.Empty<string>()
        };

        return filesToReturn;
    }
}
