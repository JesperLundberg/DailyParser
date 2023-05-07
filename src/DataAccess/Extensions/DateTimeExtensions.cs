namespace DailyParser.DataAccess.Extensions;

public static class DateTimeExtensions
{
  public static DateTime GetOnlyDate(this DateTime dateTime)
  {
    return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
  }
}
