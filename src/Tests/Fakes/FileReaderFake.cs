using DailyParser.DataAccess.Wrappers;

namespace DailyParser.Tests.Fakes;

public class FileReaderFake : IFileReader
{
    public Task<string> ReadFileAsync(string path)
    {
        return Task.FromResult("Game That I Played");
    }
}
