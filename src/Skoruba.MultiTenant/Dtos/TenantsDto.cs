using System.Collections.Generic;

namespace Skoruba.MultiTenant.Dtos
{
    public class TenantsDto
    {
        public TenantsDto()
        {
            Tenants = new List<TenantDto>();
        }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public List<TenantDto> Tenants { get; set; }
    }
}
