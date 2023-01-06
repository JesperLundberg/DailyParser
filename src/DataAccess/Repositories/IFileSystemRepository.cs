using DataAccess.Models;

namespace DataAccess.Repositories;

public interface IFileSystemRepository
{
   Task<IEnumerable<string>> GetFileListAsync(string path);

   Task<IEnumerable<FileContent>> GetFilesWithContentAsync(IEnumerable<string> files);
}