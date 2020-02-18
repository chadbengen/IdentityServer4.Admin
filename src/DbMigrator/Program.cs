using System;
using System.Threading.Tasks;
using DbMigrator.MigrateAndSeed.TenantDbContext;
using EFMigrateAndSeed;
using Finbuckle.MultiTenant.Contrib.EFCoreStore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Skoruba.DbMigrator.MigrateAndSeed;

namespace DbMigrator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //// To close the console window when debugging is finished:
            //// Debug > Options > General > Automatically close the console when debugging stops
            ////https://developercommunity.visualstudio.com/content/problem/321978/how-to-remove-exited-with-code-0-from-cmd-console.html
            
            await MigratorBuilder
                .Create()
                .SetBasePath(@"../../../../../src")
                .AddJsonFile("DbMigrator/MigrateAndSeed/connectionstrings.json", false)
                .AddJsonFile("DbMigrator/MigrateAndSeed/TenantDbContext/tenantdata.json", false)
                .AddJsonFile("DbMigrator/MigrateAndSeed/Skoruba/identitydata.json", false)
                .AddJsonFile("DbMigrator/MigrateAndSeed/Skoruba/identityserverdata.json", false)
                .Register(MigrateTenantDbContext.RegisterDependencies)
                .Register(RegisterSkoruba.RegisterDependencies)
                .Execute<Program>();

            Console.WriteLine("\npress any key to exit the process...");

            Console.ReadKey();
        }
    }
}