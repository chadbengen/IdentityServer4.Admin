using Microsoft.AspNetCore.Identity;

namespace Skoruba.IdentityServer4.Admin.EntityFramework.Shared.Entities.Identity
{
	public class UserIdentity : IdentityUser, Finbuckle.MultiTenant.Contrib.Abstractions.IHaveTenantId
	{
		public string TenantId { get; set; }
	}
}