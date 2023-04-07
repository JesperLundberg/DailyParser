namespace Parser.Services;

public interface IParserService
{
    Task<bool> ParseIntoDbAsync(string pathToFiles);
}