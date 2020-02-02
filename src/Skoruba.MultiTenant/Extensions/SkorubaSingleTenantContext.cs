using Skoruba.MultiTenant.Configuration;
using Skoruba.MultiTenant.Abstractions;
using Skoruba.MultiTenant;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    public class SkorubaSingleTenantContext : ISkorubaTenantContext
    {
        public SkorubaSingleTenantContext(MultiTenantConfiguration multiTenantConfiguration)
        {
            Tenant = new SingleTenant();
            MultiTenantConfiguration = multiTenantConfiguration;
        }

        public ISkorubaTenant Tenant { get; }
        public bool MultiTenantEnabled =>  false;
        public bool TenantResolved => false;
        public bool TenantResolutionRequired => false;
        public string TenantResolutionStrategy => "None";
        public MultiTenantConfiguration MultiTenantConfiguration { get; }
    }

    public class SingleTenant : ISkorubaTenant
    {
        public string Id { get; set; }
        public string Identifier { get; set; }
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public IDictionary<string, object> Items { get; set; } = new Dictionary<string, object>();
    }
}