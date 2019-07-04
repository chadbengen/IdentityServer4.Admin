using Skoruba.IdentityServer4.Admin.Configuration.Interfaces;

namespace Skoruba.IdentityServer4.Admin.Configuration
{
    public class TwoFactorAuthenticationConfiguration : ITwoFactorAuthenticationConfiguration
    {
        public bool IsRequired { get; set; }
        public bool EnableAuthenticatorApp { get; set; }
        public bool EnableEmailTotp { get; set; }
        public bool EnableSmsTotp { get; set; }
    }
}