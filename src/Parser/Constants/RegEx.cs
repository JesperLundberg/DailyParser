namespace DailyParser.Parser.Constants;

public static class RegEx
{
    public static string Game => @"/(?<=Vad spelar jag\?)\s*([\s\w-]*)\s*---/gm";
}
