namespace Server.Helpers;

public abstract class Constants
{
    public static class CustomSecurity
    {
        public const int PasswordLength = 10;
    }

    // NET. environments
    public static class Environments
    {
        public const string DockerDevelopment = "DockerDevelopment";
        public const string LocalIntegrationTest = "LocalIntegrationTest";
        public const string LocalDevelopment = "LocalDevelopment";
        public const string Development = "Development";
        public const string Testing = "Testing";
        public const string Staging = "Staging";
        public const string Production = "Production";
    }

    public static class Policies
    {
        public const string IsSysAdmin = "IsSysAdmin";
        public const string IsOrganizationAdmin = "IsOrganizationAdmin";
        public const string IsProfileOwner = "IsProfileOwner";
        public const string IsProfileAdmin = "IsProfileAdmin";
        public const string IsSupervisor = "IsSupervisor";
        public const string IsUser = "IsUser";
    }

    // Will be created Frontend URL for email
    public static class UrlManager
    {
        public const string ConfirmationEmail = "{0}/confirmation-email/token={1}";
        public const string ConfirmationInviteEmail = "{0}/confirmation-invite-email/token={1}";
        public const string ResetPassword = "{0}/reset-password/token={1}";
    }
}
