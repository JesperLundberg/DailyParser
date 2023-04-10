using DataAccess.Models;

namespace DataAccess.Repositories;

public class FileSystemRepository : IFileSystemRepository
{
    public Task<IEnumerable<FileNameAndPath>> GetFileListAsync(string path)
    {
        var files = GetFilesRecursively(path);

        return Task.FromResult(files);
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

    public async Task<IEnumerable<FileContent>> GetFilesWithContentAsync(IEnumerable<FileNameAndPath> files)
    {
        var fileContents = new List<FileContent>();

        foreach (var filePath in files)
        {
            using var fileStream = new StreamReader(filePath.FullPath);

            fileContents.Add(
                new FileContent
                {
                    FileName = filePath.Name,
                    Content = await fileStream.ReadToEndAsync()
                }
            );
        }

        return fileContents;
    }
}
