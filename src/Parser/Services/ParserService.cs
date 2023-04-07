﻿using DataAccess.Repositories;

namespace Parser.Services;

public class ParserService : IParserService
{
    private IDatabaseRepository DatabaseRepository { get; }
    private IFileSystemRepository FileSystemRepository { get; }

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

        return true;
    }
}