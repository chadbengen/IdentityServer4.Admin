using System.Collections.Generic;

namespace DbMigrator.MigrateAndSeed.Skoruba.Models
{
    public class Client : IdentityServer4.Models.Client
    {
        public List<Claim> ClientClaims { get; set; } = new List<Claim>();
    }
}
