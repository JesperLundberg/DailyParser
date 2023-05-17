using DailyParser.DataAccess.Models;

namespace DailyParser.Tests.Factories;

public static class FileContentFactory
{
    public static IEnumerable<FileContent> CreateValidFileContents(int howManyToCreate)
    {
      var randomDate = DateTime.Now.AddDays(new Random().Next(-500, -100));
        for (var i = 0; i < howManyToCreate; i++)
        {
            yield return new FileContent
            {
                FileName = DateTime.Now.AddDays(new Random().Next(-500, -100)).ToString("yyyy-MM-dd"),
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
                FileName = DateTime.Now.AddDays(new Random().Next(-500, -100)).ToString("yyyy-MM-dd"),
                Content = @$"This is not a valid
                  file content {Guid.NewGuid().ToString()}"
            };
        }
    }
    
    public static IEnumerable<FileContent> CreateFileWithValidDateName(int howManyToCreate)
    {
        for (var i = 0; i < howManyToCreate; i++)
        {
            // Use random to set the date to a random date
            var date = DateTime.Now.AddDays(new Random().Next(-500, -100));

            yield return new FileContent
            {
                FileName = $"/random/path/here/{date:yyyy-MM-dd}.md",
                Content = @$"This is not a valid
                  file content {Guid.NewGuid().ToString()}"
            };
        }
    }
}
