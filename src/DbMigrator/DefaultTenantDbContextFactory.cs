using Microsoft.EntityFrameworkCore;
using Finbuckle.MultiTenant.Contrib.EFCoreStore;
using Microsoft.EntityFrameworkCore.Design;

namespace DbMigrator
{
    public class DefaultTenantDbContextFactory : IDesignTimeDbContextFactory<DefaultTenantDbContext>
    {
        public DefaultTenantDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DefaultTenantDbContext>();
            optionsBuilder.UseSqlServer("Data Source=blog.db", b => b.MigrationsAssembly("DbMigrator"));
            return new DefaultTenantDbContext(optionsBuilder.Options);
        }
    }
}
