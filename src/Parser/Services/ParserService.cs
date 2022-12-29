using DataAccess.Repositories;

namespace Parser.Services;

public class ParserService
{
    private IDatabaseRepository DatabaseRepository { get; }

    public ParserService(IDatabaseRepository databaseRepository)
    {
        DatabaseRepository = databaseRepository;
    }

    public async Task<bool> ParseIntoDbAsync(string pathToFiles)
    {
        var files = await GetFileListAsync(pathToFiles);
        
        return true;
    }
}