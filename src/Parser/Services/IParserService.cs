using DailyParser.DataAccess.Models;

namespace DailyParser.Parser.Services;

public interface IParserService
{
    Task<bool> ParseIntoDbAsync(string pathToFiles);
    Task<IEnumerable<(string FileName, IEnumerable<string> ParsedText)>> ParseTextAsync(IEnumerable<FileContent> contentToBeParsed, string regexPattern);
}
