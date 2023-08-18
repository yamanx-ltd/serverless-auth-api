namespace Domain.Extensions;

public static class DateTimeExtensions
{
    public static string ToUnixTimeSeconds(this DateTime dateTime)
    {
        return new DateTimeOffset(dateTime).ToUnixTimeSeconds().ToString();
    }
}