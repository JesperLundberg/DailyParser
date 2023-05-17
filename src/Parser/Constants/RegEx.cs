namespace DailyParser.Parser.Constants;

public static class RegEx
{
    public static string Game => @"#### Vad spelar jag\?(.*)---";
    public static string Date => @"^\d{4}-\d{2}-\d{2}";
}
