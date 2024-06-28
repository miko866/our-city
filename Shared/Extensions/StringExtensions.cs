using Microsoft.IdentityModel.Tokens;

namespace Shared.Extensions;

public static class StringExtensions
{
    public static bool IsNotNull(this string str)
    {
        return !str.IsNullOrEmpty();
    }

    public static string RemoveWhiteSpaces(this string str)
    {
        return new string(str.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray());
    }

    public static string AddTimeStampToString(this string str)
    {
        return DateTimeOffset.Now.ToUnixTimeMilliseconds() + str;
    }

    public static string RemoveDashes(this string str)
    {
        return str.Replace("-", "");
    }

    public static string RandomString(this int length)
    {
        var random = new Random();

        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789?!-#@";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static string FirstCharToUpper(this string input) =>
        input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
        };

    public static bool ToBoolean(this string? str)
    {
        if (str == null)
            return false;
        return str.ToLowerInvariant() switch
        {
            "true" or "1" => true,
            _ => false
        };
    }

    public static int ToInt32(this string? str)
    {
        try
        {
            return str == null ? 0 : Int32.Parse(str);
        }
        catch (FormatException)
        {
            throw new FormatException();
        }
    }

    public static string FirstCharToUpperAsSpan(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return string.Empty;
        }
        Span<char> destination = stackalloc char[1];
        input.AsSpan(0, 1).ToUpperInvariant(destination);
        return $"{destination}{input.AsSpan(1)}";
    }
}
