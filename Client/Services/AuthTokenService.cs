namespace Client.Services;

public class AuthTokenService
{
    private string _tokenValue = string.Empty;

    public string GetAuthToken()
    {
        return _tokenValue.Replace("\"", "");
    }

    public void SetAuthToken(string authToken)
    {
        _tokenValue = authToken;
    }
}
