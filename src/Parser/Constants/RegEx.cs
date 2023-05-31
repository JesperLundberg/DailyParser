namespace DailyParser.Parser.Constants;

public class RegEx
{
    // Nonstatic to be able to be read through reflection
    public string Game => @"#### Vad spelar jag\?(.*)---";
    public string Excersise => @"\s*\[([xX]?)\]\s*Ute/Träning ✅ \d{4}-\d{2}-\d{2}\n([\s\S]*?)(?=\n\n)";

    // Static to be able to be used without creating an instance
    public static string Date => @"^\d{4}-\d{2}-\d{2}";
}
