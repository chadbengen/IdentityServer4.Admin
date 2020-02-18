using DbMigrator.MigrateAndSeed.Skoruba;
using EFMigrateAndSeed;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Skoruba.IdentityServer4.Admin.EntityFramework.Shared.DbContexts;
using Skoruba.IdentityServer4.Admin.EntityFramework.Shared.Entities.Identity;
using Skoruba.IdentityServer4.Admin.EntityFramework.Shared.Stores;
using Skoruba.IdentityServer4.Admin.EntityFramework.SqlServer.Extensions;
using System.Reflection;

namespace Skoruba.DbMigrator.MigrateAndSeed
{
    public static class RegisterSkoruba
    {
        public static void RegisterDependencies(IServiceCollection services, IConfigurationRoot configuration)
        {
            var cs = configuration.GetConnectionString("SameLocationDbConnection");
            var migrationsAssembly = typeof(DatabaseExtensions).GetTypeInfo().Assembly.GetName().Name;

            services.AddScoped<IMigrateAndSeed, MigrateAdminAuditLogDbContext>();
            services.AddDbContext<AdminAuditLogDbContext>(options => options.UseSqlServer(cs, b => b.MigrationsAssembly(migrationsAssembly)));
           
            services.AddScoped<IMigrateAndSeed, MigrateAdminIdentityDbContext>();
            services.AddDbContext<AdminIdentityDbContext>(options => options.UseSqlServer(cs, b => b.MigrationsAssembly(migrationsAssembly)));

            services.AddScoped<IMigrateAndSeed, MigrateAdminLogDbContext>();
            services.AddDbContext<AdminLogDbContext>(options => options.UseSqlServer(cs, b => b.MigrationsAssembly(migrationsAssembly)));

            services.AddScoped<IMigrateAndSeed, MigrateIdentityServerConfigurationDbContext>();
            services.AddDbContext<IdentityServerConfigurationDbContext>(options => options.UseSqlServer(cs, b => b.MigrationsAssembly(migrationsAssembly)));

            services.AddScoped<IMigrateAndSeed, MigrateIdentityServerPersistedGrantDbContext>();
            services.AddDbContext<IdentityServerPersistedGrantDbContext>(options => options.UseSqlServer(cs, b => b.MigrationsAssembly(migrationsAssembly)));

            var operationalStoreOptions = new OperationalStoreOptions();
            services.AddSingleton(operationalStoreOptions);

            var storeOptions = new ConfigurationStoreOptions();
            services.AddSingleton(storeOptions);

            services.AddIdentity<UserIdentity, UserIdentityRole>()
                .AddMultiTenantIdentityStores<DefaultMultiTenantUserStore, DefaultMultiTenantRoleStore>();
        }
    }
}
