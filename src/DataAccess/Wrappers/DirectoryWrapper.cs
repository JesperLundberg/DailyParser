namespace DailyParser.DataAccess.Wrappers;

public class DirectoryWrapper : IDirectory
{
    public IEnumerable<string> EnumerateDirectories(string path)
    {
        Console.WriteLine($"Enumerating directories in {path}");
        return Directory.EnumerateDirectories(path);
    }

    public IEnumerable<string> EnumerateFiles(string path)
    {
        Console.WriteLine($"Enumerating files in {path}");
        return Directory.EnumerateFiles(path);
    }
}
