using DailyParser.DataAccess.Models;

namespace DailyParser.DataAccess.Repositories;

public interface IFileSystemRepository
{
    Task<IEnumerable<FileNameAndPath>> GetFileListAsync(string path);
    Task<FileContent> GetFileWithContentAsync(FileNameAndPath file);
    Task<IEnumerable<FileContent>> GetFilesWithContentAsync(IEnumerable<FileNameAndPath> files);
}
