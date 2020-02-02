using Skoruba.MultiTenant.Abstractions;
using System.Collections.Generic;

namespace Skoruba.MultiTenant.EntityFramework
{
    public class TenantEntity : ISkorubaTenant
    {
        public string ConnectionString { get; set; }
        public string Id { get; set; }
        public string Identifier { get; set; }
        public IDictionary<string, object> Items { get; set; }
        public string Name { get; set; }
    }

}