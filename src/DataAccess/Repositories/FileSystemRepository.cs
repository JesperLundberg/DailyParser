using DailyParser.DataAccess.Models;
using DailyParser.DataAccess.Wrappers;

namespace DailyParser.DataAccess.Repositories;

public class FileSystemRepository : IFileSystemRepository
{
    private IDirectory Directory { get; set; }
    private IFileReader FileReader { get; set; }

    public FileSystemRepository(IDirectory directory, IFileReader fileReader)
    {
        Directory = directory;
        FileReader = fileReader;
    }

    public Task<IEnumerable<FileNameAndPath>> GetFileListAsync(string path)
    {
        var files = GetFilesRecursively(path);

        return Task.FromResult(files);
    }

    public async Task<FileContent> GetFileWithContentAsync(FileNameAndPath file)
    {
        var fileContent = await FileReader.ReadFileAsync(file.FullPath);

        return new FileContent { FileName = file.Name, Content = fileContent };
    }

    public async Task<IEnumerable<FileContent>> GetFilesWithContentAsync(
        IEnumerable<FileNameAndPath> files
    )
    {
        var fileTasks = new List<Task<FileContent>>();

        foreach (var file in files)
        {
            fileTasks.Add(GetFileWithContentAsync(file));
        }

        await Task.WhenAll(fileTasks);

        return fileTasks.Select(x => x.Result);
    }

    private IEnumerable<FileNameAndPath> GetFilesRecursively(string path)
    {
        var files = Directory
            .EnumerateFiles(path)
            .Select(x => new FileNameAndPath { Name = x, FullPath = Path.Join(path, x) })
            .ToList();

        var subDirectories = Directory.EnumerateDirectories(path);

        if (!subDirectories.Any())
        {
            return files;
        }

        foreach (var directory in subDirectories)
        {
            files.AddRange(GetFilesRecursively(Path.Join(path, directory)));
        }

        return files;
    }
}
