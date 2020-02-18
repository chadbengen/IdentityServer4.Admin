using EFMigrateAndSeed;
using EFMigrateAndSeed.Dependency;
using Skoruba.IdentityServer4.Admin.EntityFramework.Shared.DbContexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Skoruba.DbMigrator.MigrateAndSeed;

namespace DbMigrator.MigrateAndSeed.Skoruba
{
    [DependsOn(typeof(MigrateAdminLogDbContext))]
    public class MigrateAdminAuditLogDbContext : MigrateAndSeedBase
    {
        public MigrateAdminAuditLogDbContext(ILogger<MigrateAdminAuditLogDbContext> logger) : base(logger)
        {
        }

        public override Task Migrate(IServiceCollection services)
        {
            return MigrateDbContext<AdminAuditLogDbContext>(services.BuildServiceProvider());
        }

        public override Task Seed(IServiceCollection services, IConfigurationRoot configurationRoot)
        {
            _logger.LogInformation("Seeded {SourceContext}, no data to seed.");
            return Task.CompletedTask;
        }
    }
}
