using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Skoruba.MultiTenant.Abstractions;
using Skoruba.MultiTenant.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skoruba.MultiTenant.EntityFramework.Services
{
    public class TenantService : ITenantService
    {
        private readonly DefaultTenantDbContext _dbContext;
        private readonly IMapper _mapper;

        public TenantService(DefaultTenantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TenantDto> AddTenantAsync(TenantDto tenantDto)
        {
            tenantDto.Id = Guid.NewGuid().ToString();

            //set tenant properties
            var newTenant = new TenantEntity()
            {
                Id = tenantDto.Id,
                Name = tenantDto.Name,
                Identifier = tenantDto.Identifier,
                ConnectionString = tenantDto.ConnectionString,
                Items = tenantDto.Items
            };

            _dbContext.Tenants.Add(newTenant);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<TenantDto>(newTenant);
        }

        public async Task<TenantResult> DisableTenantAsync(string id)
        {
            var tenant = await _dbContext.Tenants.FirstOrDefaultAsync(a => a.Id == id);

            tenant.SetIsActive(false);

            await _dbContext.SaveChangesAsync();
            return TenantResult.Success();
        }

        public async Task<TenantDto> GetTenantAsync(string id)
        {
            var tenant = await _dbContext.Tenants
                .ProjectTo<TenantDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Id == id);

            //if (tenant == null) throw new UserFriendlyErrorPageException(string.Format(TenantServiceResources.TenantDoesNotExist().Description, id), TenantServiceResources.TenantDoesNotExist().Description);

            return tenant;
        }

        public async Task<TenantsDto> GetTenantsAsync(int page = 1, int pageSize = 10)
        {
            var skip = page * pageSize;
            var take = pageSize;

            var tenants = await _dbContext.Tenants
                .Skip(skip)
                .Take(take)
                .ProjectTo<TenantDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            
            var totalCount = await _dbContext.Tenants.CountAsync();

            var tenantsDto = new TenantsDto()
            {
                PageSize = pageSize,
                Tenants = tenants,
                TotalCount = totalCount
            };

            return tenantsDto;
        }
        public Task<TenantResult> UpdateTenantAsync()
        {
            throw new NotImplementedException();
        }
    }
}
