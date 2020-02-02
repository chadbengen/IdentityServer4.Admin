using System;
using System.Collections.Generic;
using System.Text;

namespace Skoruba.MultiTenant.Dtos
{
    public class TenantDto
    {
        public bool IsDefaultId => EqualityComparer<string>.Default.Equals(Id, Guid.Empty.ToString());
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool RequireTwoFactorAuthentication { get; set; }
        public string ConnectionString { get; set; }
        public string Identifier { get; set; }
        public IDictionary<string, object> Items { get; set; }
    }
}
