namespace DailyParser.DataAccess.Wrappers;

public class FileReaderWapper : IFileReader
{
    public Task<string> ReadFileAsync(string path)
    {
        using var FileStream = new StreamReader(path); 

        return FileStream.ReadToEndAsync();
    }  
}
