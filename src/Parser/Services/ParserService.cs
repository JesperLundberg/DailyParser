using DailyParser.DataAccess.Repositories;

namespace DailyParser.Parser.Services;

public class ParserService : IParserService
{
    private IDatabaseRepository DatabaseRepository { get; init; }
    private IFileSystemRepository FileSystemRepository { get; init; }

    public ParserService(IDatabaseRepository databaseRepository,
        IFileSystemRepository fileSystemRepository)
    {
        DatabaseRepository = databaseRepository;
        FileSystemRepository = fileSystemRepository;
    }

    public async Task<bool> ParseIntoDbAsync(string pathToFiles)
    {
        var files = await FileSystemRepository.GetFileListAsync(pathToFiles);

        // TODO: This can be done async and in parallell
        var filesWithContent = await FileSystemRepository.GetFilesWithContentAsync(files);
        
        // TODO: Save everything as a batch when it's done
        await DatabaseRepository.SaveFilesWithContentAsync(filesWithContent);

        return true;
    }
}
