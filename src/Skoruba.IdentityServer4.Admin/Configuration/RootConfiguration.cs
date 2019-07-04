using Microsoft.Extensions.Options;
using Skoruba.IdentityServer4.Admin.Configuration.Interfaces;

namespace Skoruba.IdentityServer4.Admin.Configuration
{
    public class RootConfiguration : IRootConfiguration
    {
        public IAdminConfiguration AdminConfiguration { get; set; }
        public ITwoFactorAuthenticationConfiguration TwoFactorAuthenticationConfiguration { get; }

        public RootConfiguration(IOptions<AdminConfiguration> adminConfiguration, IOptions<TwoFactorAuthenticationConfiguration> twoFactorAuthenticationConfiguration)
        {
            AdminConfiguration = adminConfiguration.Value;
            TwoFactorAuthenticationConfiguration = twoFactorAuthenticationConfiguration.Value;
        }
    }
}