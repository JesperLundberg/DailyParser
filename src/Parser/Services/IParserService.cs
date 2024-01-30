using DailyParser.DataAccess.Models;
using DailyParser.Models.Models;

namespace DailyParser.Parser.Services;

public interface IParserService
{
    Task<bool> ParseIntoDbAsync(string pathToFiles);
    Task<IEnumerable<ParsedText>> ParseTextAsync(
        IEnumerable<FileContent> contentToBeParsed,
        (string Name, string? Pattern) regex
    );
}
