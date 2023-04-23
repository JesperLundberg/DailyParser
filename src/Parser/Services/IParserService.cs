using DailyParser.DataAccess.Models;

namespace DailyParser.Parser.Services;

public interface IParserService
{
    Task<bool> ParseIntoDbAsync(string pathToFiles);
    Task<IEnumerable<(string FileName, string ParsedText)>> ParseText(IEnumerable<FileContent> contentToBeParsed, string regexPattern);
}
