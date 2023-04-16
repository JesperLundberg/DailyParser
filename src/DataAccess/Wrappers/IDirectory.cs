namespace DailyParser.DataAccess.Wrappers;

public interface IDirectory
{
    IEnumerable<string> EnumerateFiles(string path);
    IEnumerable<string> EnumerateDirectories(string path);
}
