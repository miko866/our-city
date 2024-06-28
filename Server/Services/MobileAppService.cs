using Server.Security;

namespace Server.Services;

#region Interface
public interface IMobileAppService
{
    Task<string> CreateMobileAppToken();
}
#endregion Interface

public class MobileAppService : IMobileAppService
{
    private readonly IJwtGenerator _jwtGenerator;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="jwtGenerator"></param>
    public MobileAppService(IJwtGenerator jwtGenerator)
    {
        _jwtGenerator = jwtGenerator;
    }

    #region Implementation

    #region Public methods

    public async Task<string> CreateMobileAppToken()
    {
        string token = await _jwtGenerator.GenerateMobileAppJwtToken().ConfigureAwait(false);

        return token;
    }

    #endregion Public methods

    #endregion  Implementation
}
