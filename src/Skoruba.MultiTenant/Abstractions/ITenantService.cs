using Skoruba.MultiTenant.Dtos;
using System.Threading.Tasks;

namespace Skoruba.MultiTenant.Abstractions
{
    public interface ITenantService
    {
        Task<TenantDto> GetTenantAsync(string id);

        Task<TenantsDto> GetTenantsAsync(int page = 1, int pageSize = 10);

        Task<TenantResult> UpdateTenantAsync();

        Task<TenantDto> AddTenantAsync(TenantDto dto);

        Task<TenantResult> DisableTenantAsync(string id);
    }

}
