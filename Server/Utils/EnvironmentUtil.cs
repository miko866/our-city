namespace Server.Utils;

public static class EnvironmentUtil
{
    public static bool AllowDebugForEnvironments(string environmentName)
    {
        return environmentName
            is Helpers.Constants.Environments.LocalDevelopment
                or Helpers.Constants.Environments.Development
                or Helpers.Constants.Environments.Testing
                or Helpers.Constants.Environments.LocalIntegrationTest;
    }
}
