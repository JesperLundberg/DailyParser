using DailyParser.DataAccess.Wrappers;

namespace DailyParser.Tests.Fakes;

public class FileReaderFake : IFileReader
{
    private static int counter = 0;

    public Task<string> ReadFileAsync(string path)
    {
        counter++;

        return Task.FromResult($"Game That I Played {counter}");
    }
}
