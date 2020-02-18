using Microsoft.AspNetCore.Identity;

namespace Skoruba.IdentityServer4.Admin.EntityFramework.Shared.Entities.Identity
{
	public class UserIdentityRole : IdentityRole, Finbuckle.MultiTenant.Contrib.Abstractions.IHaveTenantId
	{
		public string TenantId { get; set; }
	}
}