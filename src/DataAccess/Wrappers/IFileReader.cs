namespace DailyParser.DataAccess.Wrappers;

public interface IFileReader
{
    Task<string> ReadFileAsync(string path);
}
