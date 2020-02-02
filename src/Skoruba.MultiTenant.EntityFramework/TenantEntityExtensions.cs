using Skoruba.MultiTenant.Extensions;

namespace Skoruba.MultiTenant.EntityFramework
{
    public static class TenantEntityExtensions
    {
        public static bool? GetRequiresTwoFactorAuthentication(this TenantEntity tenant)
        {
            return tenant.Items.GetRequiresTwoFactorAuthentication();
        }
        public static void SetRequiresTwoFactorAuthentication(this TenantEntity tenant, bool? value)
        {
            tenant.Items.SetRequiresTwoFactorAuthentication(value);
        }


        public static bool? GetIsActive(this TenantEntity tenant)
        {
            return tenant.Items.GetIsActive();
        }
        public static void SetIsActive(this TenantEntity tenant, bool? value)
        {
            tenant.SetIsActive(value);
        }

    }

}