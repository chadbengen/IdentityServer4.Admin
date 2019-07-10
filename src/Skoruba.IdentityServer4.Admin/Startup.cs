﻿using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity;
using Skoruba.IdentityServer4.Admin.Configuration.Interfaces;
using Skoruba.IdentityServer4.Admin.DependencyInjection;
using Skoruba.IdentityServer4.Admin.EntityFramework.Shared.DbContexts;
using Skoruba.IdentityServer4.Admin.EntityFramework.Shared.Entities.Identity;
using Skoruba.IdentityServer4.Admin.Helpers;
using MediatR;
using Skoruba.IdentityServer4.Audit.Core;
using Skoruba.IdentityServer4.Audit.EntityFramework.Handlers;
using Skoruba.IdentityServer4.Audit.Sink.DependencyInjection;
using Skoruba.IdentityServer4.Audit.EntityFramework.DependencyInjection;
using Skoruba.IdentityServer4.Admin.Configuration.Constants;

namespace Skoruba.IdentityServer4.Admin
{
    public class Startup
    {
        private readonly ILoggerFactory _loggerFactory;

        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var builder = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();

            HostingEnvironment = env;
            _loggerFactory = loggerFactory;
        }

        public IConfigurationRoot Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Get Configuration
            services.ConfigureRootConfiguration(Configuration);
            var rootConfiguration = services.BuildServiceProvider().GetService<IRootConfiguration>();

            ///// Manual configuration
            //services
            //    .AddCustomConfiguration
            //        <AdminIdentityDbContext, IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext, AdminLogDbContext>
            //        (HostingEnvironment,
            //        StartupHelpers.DefaultIdentityDbContextOptions(Configuration),
            //        StartupHelpers.DefaultIdentityServerConfigurationOptions(Configuration),
            //        StartupHelpers.DefaultIdentityServerOperationalStoreOptions(Configuration),
            //        StartupHelpers.DefaultLogDbContextOptions(Configuration))
            //    .AddCustomIdentity
            //        <AdminIdentityDbContext, UserIdentity, UserIdentityRole>
            //        (StartupHelpers.DefaultIdentityOptions(Configuration))
            //    .AddCustomAdminAspNetIdentityServices
            //        <AdminIdentityDbContext, IdentityServerPersistedGrantDbContext, UserDto<string>, string, RoleDto<string>, string, string, string,
            //        UserIdentity, UserIdentityRole, string, UserIdentityUserClaim, UserIdentityUserRole,
            //        UserIdentityUserLogin, UserIdentityRoleClaim, UserIdentityUserToken,
            //        UsersDto<UserDto<string>, string>, RolesDto<RoleDto<string>, string>, UserRolesDto<RoleDto<string>, string, string>,
            //        UserClaimsDto<string>, UserProviderDto<string>, UserProvidersDto<string>, UserChangePasswordDto<string>,
            //        RoleClaimsDto<string>, UserClaimDto<string>, RoleClaimDto<string>>();

            ///// Single tenant configuration (default)
            //services.AddSingleTenantConfiguration(HostingEnvironment,
            //    StartupHelpers.DefaultIdentityDbContextOptions(Configuration),
            //    StartupHelpers.DefaultIdentityServerConfigurationOptions(Configuration),
            //    StartupHelpers.DefaultIdentityServerOperationalStoreOptions(Configuration),
            //    StartupHelpers.DefaultLogDbContextOptions(Configuration),
            //    StartupHelpers.DefaultIdentityOptions(Configuration));

            /// Multi Tenant configuration
            services.AddMultiTenantConfiguration(HostingEnvironment,
                StartupHelpers.DefaultIdentityDbContextOptions(Configuration),
                StartupHelpers.DefaultIdentityServerConfigurationOptions(Configuration),
                StartupHelpers.DefaultIdentityServerOperationalStoreOptions(Configuration),
                StartupHelpers.DefaultLogDbContextOptions(Configuration),
                StartupHelpers.DefaultIdentityOptions(Configuration));

            // Add Asp.Net Core Identity Configuration and OpenIdConnect auth as well
            services.AddAuthenticationServices(HostingEnvironment, rootConfiguration.AdminConfiguration);

            // Add exception filters in MVC
            services.AddMvcExceptionFilters();

            services.AddIdentityServer4Auditing()
                        .AddIdentityServerOptions()
                        .AddConsoleSink()
                        .AddSerilogSinkWithDbContext(Configuration.GetConnectionString(ConfigurationConsts.IdentityDbConnectionStringKey), HostingEnvironment.EnvironmentName)
                        .AddDefaultIdentityServer4Sink();
            
            // Add authorization policies for MVC
            services.AddAuthorizationPolicies();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.AddLogging(loggerFactory, Configuration);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Add custom security headers
            app.UseSecurityHeaders();

            app.UseStaticFiles();

            // Use authentication and for integration tests use custom middleware which is used only in Staging environment
            app.ConfigureAuthenticationServices(env);

            // Use Localization
            app.ConfigureLocalization();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}