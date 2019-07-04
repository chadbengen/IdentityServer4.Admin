using Microsoft.AspNetCore.Identity;
using Skoruba.IdentityServer4.Admin.EntityFramework.Shared.Entities.Identity;
using Skoruba.IdentityServer4.Admin.EntityFramework.Shared.Managers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoruba.IdentityServer4.Admin.EntityFramework.Shared.Validators
{
    public class MightRequireTwoFactorAuthentication<TUser> : IMultiUserValidator<TUser>
        where TUser : IdentityUser
    {
        private readonly ITenantManager _tenantManager;
        private readonly ITwoFactorAuthenticationConfiguration _configuration;

        public MightRequireTwoFactorAuthentication(IEnumerable<ITwoFactorAuthenticationConfiguration> configuration, IEnumerable<ITenantManager> tenantManagers)
        {
            // Admin has a 2fa configuration setting to allow a global
            //   2fa requirement for creating users.  This configuration
            //   object is injected during the Builder configuration.
            //
            // STS does not have the 2fa configuration setting becuase
            //   that is a create/update responsibility and STS is
            //   only responsible for logging in and user profiles.
            //   Therefore, no configuration will be injected.
            //
            // Becuase UserManager is used by both apps we need
            //   to accomodate a null ITwoFactorAuthenticationConfiguration
            //   object in case it is executing on STS.
            //
            // Likewise, the TenantManager might not be injected if
            //   a single tenant configuration is desired.  Therefore,
            //   a null TenantManager needs to be accommodated.
            //
            // Using IEnumerable accomodates for nulls.
            //
            // However, this seems like a fragile assumption.  What
            //   is stopping STS from creating a user other than
            //   the controller logic?
            //
            // This really seems like a domain (Business logic) responsibility and
            //   somehow we need to figure out the requirement on that level.
            //   Do we share a configuration object between all the web apps?
            //

            _configuration = configuration.FirstOrDefault();

            _tenantManager = tenantManagers.FirstOrDefault();
        }

        public async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user)
        {
            // The validator is always registered and therefore always validated.
            // The valiator requires TwoFactorEnabled to be true if the one of
            // the following condiations are satisifed:
            //      1. User has it set to true
            //      2. The User has the email "admin@example.com"  (debugging only)
            //      3. The appsettings globally has it set to required OR
            //         MultiTenant is implemented and the tenantManager is injected and looks up tenant setting

            if (user.TwoFactorEnabled)
            {
                return IdentityResult.Success;
            }

#if DEBUG
            // Ignore seeded email
            if (user.Email == "admin@example.com")
            {
                return IdentityResult.Success;
            }
#endif

            // Check config and if it's true then the validation failed
            if (_configuration.IsRequired)
            {
                return IdentityResult.Failed(new IdentityError { Code = "2fa_Required", Description = "Two factor authentication is required." });
            }

            // Check if required on tenant
            if (_tenantManager != null && user as MultiTenantUserIdentity != null)
            {
                if (!await _tenantManager.IsTwoFactorAuthenticationRequiredAsync((user as MultiTenantUserIdentity).TenantId))
                {
                    return IdentityResult.Success;
                }
            }

            return IdentityResult.Failed(new IdentityError { Code = "2fa_Required", Description = "Two factor authentication is required." });
        }
    }
}