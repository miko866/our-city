namespace Server.Utils;

// Needed, because .net is not able to correctly URL encode/decode his own tokens
// Therefore we now have our own stuff ;-)
// https://petedavis.io/blog/url-encoding-password-reset-or-email-confirmation-tokens
public static class Base64Utility
{
    private static readonly char[] Padding = ['='];

    public static string UrlSafeEncode(this string base64String)
    {
        return base64String.TrimEnd(Padding).Replace('+', '-').Replace('/', '_');
    }

    public static string UrlSafeDecode(this string urlSafeBase64String)
    {
        string base64String = urlSafeBase64String.Replace('_', '/').Replace('-', '+');
        switch (urlSafeBase64String.Length % 4)
        {
            case 2:
                base64String += "==";
                break;
            case 3:
                base64String += "=";
                break;
        }
        return base64String;
    }
}
