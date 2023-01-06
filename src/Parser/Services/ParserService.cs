using DataAccess.Repositories;

namespace Parser.Services;

public class ParserService
{
    private IDatabaseRepository DatabaseRepository { get; }
    private IFileSystemRepository FileSystemRepository { get; }

    public ParserService(IDatabaseRepository databaseRepository)
    {
        DatabaseRepository = databaseRepository;
    }

    public async Task<bool> ParseIntoDbAsync(string pathToFiles)
    {
        var files = await FileSystemRepository.GetFileListAsync(pathToFiles);

        var filesWithContent = await FileSystemRepository.GetFilesWithContentAsync(files);
        
        return true;
    }
}