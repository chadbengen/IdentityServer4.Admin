using Finbuckle.MultiTenant;
using System;
using Skoruba.MultiTenant.Extensions;

namespace Skoruba.MultiTenant.Finbuckle.Extensions
{
    public static class TenantInfoExtensions
    {
        public static bool? GetRequiresTwoFactorAuthentication(this TenantInfo tenantInfo)
        {
            return tenantInfo.Items.GetRequiresTwoFactorAuthentication();
        }
        public static void SetRequiresTwoFactorAuthentication(this TenantInfo tenantInfo, bool? value)
        {
            tenantInfo.Items.SetRequiresTwoFactorAuthentication(value);
        }


        public static bool? GetIsActive(this TenantInfo tenantInfo)
        {
            return tenantInfo.Items.GetIsActive();
        }
        public static void SetIsActive(this TenantInfo tenantInfo, bool? value)
        {
            tenantInfo.SetIsActive(value);
        }

    }
}