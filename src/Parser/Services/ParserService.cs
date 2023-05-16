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
        // TODO: Make this work with all types of parsers. Rewrite this to use IParsers like in Edströms
        var files = await FileSystemRepository.GetFileListAsync(pathToFiles);
        Console.WriteLine($"Found {files.Count()} files");

        var filesWithContent = await FileSystemRepository.GetFilesWithContentAsync(files);

        var parsedText = await ParseTextAsync(filesWithContent, RegEx.Game);

        var result = await DatabaseRepository.CreateParsedDayAsync(parsedText);

        return result;
    }

    public Task<IEnumerable<ParsedText>> ParseTextAsync(
        IEnumerable<FileContent> contentToBeParsed,
        string regexPattern
    )
    {
        // Get the parsed text from the content
        var parsedText = contentToBeParsed.Select(
            x =>
                new ParsedText
                {
                    Name = x.FileName,
                    Texts = Regex
                        // Match the regex pattern and use singleline as the string is a single line
                        .Match(x.Content, regexPattern, RegexOptions.Singleline)
                        // The first group contains the correct text
                        .Groups[1].Value
                        .Replace(" ", string.Empty)
                        // Split the text into lines and remove empty lines
                        .Split(Environment.NewLine)
                        .Where(y => !string.IsNullOrWhiteSpace(y))
                        .AsEnumerable()
                }
        );

        return Task.FromResult(parsedText);
    }
}
