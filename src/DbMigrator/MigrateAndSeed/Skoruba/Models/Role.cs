using System.Collections.Generic;

namespace DbMigrator.MigrateAndSeed.Skoruba.Models
{
    public class Role
    {
        public string Name { get; set; }
        public string TenantId { get; set; }
        public List<Claim> Claims { get; set; } = new List<Claim>();
    }
}
