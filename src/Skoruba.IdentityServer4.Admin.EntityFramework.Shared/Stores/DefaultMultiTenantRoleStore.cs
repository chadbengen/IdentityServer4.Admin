using Finbuckle.MultiTenant.Contrib.Abstractions;
using Finbuckle.MultiTenant.Contrib.Identity.Stores;
using Microsoft.AspNetCore.Identity;
using Skoruba.IdentityServer4.Admin.EntityFramework.Shared.DbContexts;
using Skoruba.IdentityServer4.Admin.EntityFramework.Shared.Entities.Identity;

namespace Skoruba.IdentityServer4.Admin.EntityFramework.Shared.Stores
{
    public class DefaultMultiTenantUserStore :
        MultiTenantUserStore<
            UserIdentity,
            UserIdentityRole,
            AdminIdentityDbContext,
            string,
            UserIdentityUserClaim,
            UserIdentityUserRole,
            UserIdentityUserLogin,
            UserIdentityUserToken,
            UserIdentityRoleClaim>
    {
        public DefaultMultiTenantUserStore(AdminIdentityDbContext context,
            ITenantContext tenantContext,
            IdentityErrorDescriber describer = null)
            : base(context, tenantContext, describer)
        {
        }
    }
}
