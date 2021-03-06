﻿using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Skoruba.IdentityServer4.Admin.EntityFramework.Shared.DbContexts;
using Skoruba.IdentityServer4.Admin.EntityFramework.Shared.Entities.Identity;
using Skoruba.IdentityServer4.STS.Identity.Configuration;
using Skoruba.IdentityServer4.STS.Identity.Configuration.Constants;
using Skoruba.IdentityServer4.STS.Identity.Configuration.Interfaces;
using Skoruba.IdentityServer4.STS.Identity.Helpers;
using Skoruba.MultiTenant;
using Skoruba.MultiTenant.Finbuckle.Strategies;
using Skoruba.MultiTenant.IdentityServer;

namespace Skoruba.IdentityServer4.STS.Identity
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var rootConfiguration = CreateRootConfiguration();
            services.AddSingleton(rootConfiguration);

            // Register DbContexts for IdentityServer and Identity
            RegisterDbContexts(services);

            // Add email senders which is currently setup for SendGrid and SMTP
            services.AddEmailSenders(Configuration);

            // Add services for authentication, including Identity model and external providers
            RegisterAuthentication(services);

            // Add all dependencies for Asp.Net Core Identity in MVC - these dependencies are injected into generic Controllers
            // Including settings for MVC and Localization
            // If you want to change primary keys or use another db model for Asp.Net Core Identity:
            services.AddMvcWithLocalization<UserIdentity, string>(Configuration);

            // Add authorization policies for MVC
            RegisterAuthorization(services);

            RegisterMultiTenantConfiguration(services);

            services.AddIdSHealthChecks<IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext, AdminIdentityDbContext>(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Add custom security headers
            app.UseSecurityHeaders();

            app.UseStaticFiles();

            app.UseRouting();

            // configure default multitenant middleware before authentication
            UsePreAuthenticationMultitenantMiddleware(app);

            UseAuthentication(app);

            // configure custom multitenant middleware for claims after authentication
            UsePostAuthenticationMultitenantMiddleware(app);

            app.UseMvcLocalizationServices();

            app.UseAuthorization();

            app.UseEndpoints(endpoint =>
            {
                endpoint.MapDefaultControllerRoute();
                endpoint.MapHealthChecks("/health", new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }

        public virtual void RegisterDbContexts(IServiceCollection services)
        {
            services.RegisterDbContexts<AdminIdentityDbContext, IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext>(Configuration);
        }

        public virtual void RegisterAuthentication(IServiceCollection services)
        {
            services.AddAuthenticationServices<AdminIdentityDbContext, UserIdentity, UserIdentityRole>(Configuration);
            services.AddIdentityServer<IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext, UserIdentity>(Configuration);
        }

        public virtual void RegisterAuthorization(IServiceCollection services)
        {
            var rootConfiguration = CreateRootConfiguration();
            services.AddAuthorizationPolicies(rootConfiguration);
        }
        public virtual void RegisterMultiTenantConfiguration(IServiceCollection services) {
 
            // If single tenant app then change to false and remove app configuration
            services.AddMultiTenant(true)
                // required if using app.AddMultiTenantFromForm()
                .RegisterConfiguration(Configuration.GetSection("MultiTenantConfiguration"))
                // custom store
                .WithEFCacheStore(options => options.UseSqlServer(Configuration.GetConnectionString("TenantsDbConnection")))
                // custom strategy to get tenant from form data at login
                .WithStrategy<FormStrategy>(ServiceLifetime.Singleton)
                // dont require tenant resolution for identity endpoints
                .RegisterTenantIsRequiredValidation<TenantNotRequiredForIdentityServerEndpoints>()
            ;

        }

        public virtual void UseAuthentication(IApplicationBuilder app)
        {
            app.UseIdentityServer();
        }

        public virtual void UsePreAuthenticationMultitenantMiddleware(IApplicationBuilder app)
        {
            // configure default multitenant middleware before authentication
            app.UseMultiTenant();
        }
        public virtual void UsePostAuthenticationMultitenantMiddleware(IApplicationBuilder app)
        {
            // configure custom multitenant middleware for claims after authentication
            app.UseMultiTenantFromClaims();
        }
        protected IRootConfiguration CreateRootConfiguration()
        {
            var rootConfiguration = new RootConfiguration();
            Configuration.GetSection(ConfigurationConsts.AdminConfigurationKey).Bind(rootConfiguration.AdminConfiguration);
            Configuration.GetSection(ConfigurationConsts.RegisterConfigurationKey).Bind(rootConfiguration.RegisterConfiguration);
            return rootConfiguration;
        }
    }
}
