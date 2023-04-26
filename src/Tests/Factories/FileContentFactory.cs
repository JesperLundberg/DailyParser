using DailyParser.DataAccess.Models;

namespace DailyParser.Tests.Factories;

public static class FileContentFactory
{
    public static IEnumerable<FileContent> CreateValidFileContents(int howManyToCreate)
    {
        for (var i = 0; i < howManyToCreate; i++)
        {
            yield return new FileContent
            {
                FileName = Guid.NewGuid().ToString(),
                Content =
                    @$"
                    Text that is not used with whitespaces after     
                    #### Vad spelar jag?
                    Primordia
                    Outcast
                    ---
                    other irrelevant text here"
            };
        }
    }

    public static IEnumerable<FileContent> CreateInvalidFileContents(int howManyToCreate)
    {
        for (var i = 0; i < howManyToCreate; i++)
        {
            yield return new FileContent
            {
                FileName = Guid.NewGuid().ToString(),
                Content = @$"This is not a valid
                  file content {Guid.NewGuid().ToString()}"
            };
        }
    }
}
