namespace WaterDesk.Services;

public static class DateTimeExtensions
{
    public static DateTime ToDateTime(this long? unixTime)
    {
        if (unixTime == null)
            return DateTime.MinValue;

        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return epoch.AddSeconds(unixTime.Value);
    }

    public static long ToUnixTime(this DateTime date)
    {
        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return Convert.ToInt64((date - epoch).TotalSeconds);
    }
}