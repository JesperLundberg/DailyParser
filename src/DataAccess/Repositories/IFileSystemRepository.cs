using DataAccess.Models;

namespace DataAccess.Repositories;

public interface IFileSystemRepository
{
   Task<IEnumerable<FileNameAndPath>> GetFileListAsync(string path);

   Task<IEnumerable<FileContent>> GetFilesWithContentAsync(IEnumerable<FileNameAndPath> files);
}
