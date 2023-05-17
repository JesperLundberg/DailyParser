namespace DailyParser.DataAccess.Wrappers;

public class FileReaderWapper : IFileReader
{
    public async Task<string> ReadFileAsync(string path)
    {
        using var FileStream = new StreamReader(path); 

        return await FileStream.ReadToEndAsync();
    }  
}
