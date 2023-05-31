using System.Text.RegularExpressions;
using DailyParser.DataAccess.Models;
using DailyParser.DataAccess.Repositories;
using DailyParser.Models.Models;
using DailyParser.Parser.Constants;

namespace DailyParser.Parser.Services;

public class ParserService : IParserService
{
    private IDatabaseRepository DatabaseRepository { get; init; }
    private IFileSystemRepository FileSystemRepository { get; init; }

    public ParserService(
        IDatabaseRepository databaseRepository,
        IFileSystemRepository fileSystemRepository
    )
    {
        DatabaseRepository = databaseRepository;
        FileSystemRepository = fileSystemRepository;
    }

    public async Task<bool> ParseIntoDbAsync(string pathToFiles)
    {
        var files = await FileSystemRepository.GetFileListAsync(pathToFiles);

        var filesWithContent = await FileSystemRepository.GetFilesWithContentAsync(files);

        var parsedText = new List<ParsedText>();

        foreach (var regEx in GetAllRegEx())
        {
            parsedText.AddRange(await ParseTextAsync(filesWithContent, regEx!));
        }

        var result = await DatabaseRepository.CreateParsedDayAsync(parsedText);

        return result;
    }

    public Task<IEnumerable<ParsedText>> ParseTextAsync(
        IEnumerable<FileContent> contentToBeParsed,
        string regexPattern
    )
    {
        // Get the parsed text from the content
        var parsedText = contentToBeParsed
            // Remove all files that does not have a name that is a date in the format described in Regex.Date
            .Where(x => Regex.IsMatch(x.FileName, RegEx.Date))
            .Select(
                x =>
                    new ParsedText
                    {
                        // Strip everything but the filename
                        Name = x.FileName,
                        Texts = Regex
                            // Match the regex pattern and use singleline as the string is a single line
                            .Match(x.Content, regexPattern, RegexOptions.Singleline)
                            // The first group contains the correct text
                            .Groups[1].Value
                            // Split the text into lines and remove empty lines
                            .Split(Environment.NewLine)
                            .Where(y => !string.IsNullOrWhiteSpace(y))
                            .Select(y => y.Trim())
                            .AsEnumerable()
                    }
            );

        return Task.FromResult(parsedText);
    }

    private IEnumerable<string?> GetAllRegEx()
    {
        var regEx = new RegEx();
        var regExType = typeof(RegEx).GetProperties();

        return regExType.Select(x => x.GetValue(regEx)!.ToString());
    }
}
