namespace Shared.Extensions;

public static class DateTimeExtensions
{
    public static bool ValidAge(this DateTime date, int maxAge)
    {
        int currentYear = DateTime.Now.Year;
        int incomingYear = date.Year;

        if (incomingYear <= currentYear && incomingYear > (currentYear - maxAge))
        {
            return true;
        }

        return false;
    }

    public static bool ValidAge(this DateTime? date, int maxAge)
    {
        if (date.IsNull())
            return true;
        DateTime? newDateTime = (DateTime)date!;
        return ValidAge(newDateTime, maxAge);
    }
}
