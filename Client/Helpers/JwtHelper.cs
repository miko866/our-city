namespace Client.Helpers;

public static class JwtHelper
{
    public static byte[] FromBase64Url(string base64Url)
    {
        string padded =
            base64Url.Length % 4 == 0 ? base64Url : string.Concat(base64Url, "====".AsSpan(base64Url.Length % 4));
        string base64 = padded.Replace("_", "/").Replace("-", "+");
        return Convert.FromBase64String(base64);
    }

    public static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }
        return Convert.FromBase64String(base64);
    }

    private static readonly char[] separator = [','];

    public static bool CheckStringList(Dictionary<string, object>? keyValuePairs, string roleClaimType)
    {
        string[] splitHeader = keyValuePairs!
            [roleClaimType]
            .ToString()!
            .Split(separator, StringSplitOptions.RemoveEmptyEntries);

        return Convert.ToInt32(splitHeader.Length > 1) == 1;
    }
}
