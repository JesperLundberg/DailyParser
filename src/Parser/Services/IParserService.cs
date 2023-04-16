namespace DailyParser.Parser.Services;

public interface IParserService
{
    Task<bool> ParseIntoDbAsync(string pathToFiles);
}
