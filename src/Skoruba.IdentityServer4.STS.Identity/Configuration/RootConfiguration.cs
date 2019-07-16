﻿using Microsoft.Extensions.Options;
using Skoruba.IdentityServer4.STS.Identity.Configuration.Intefaces;

namespace Skoruba.IdentityServer4.STS.Identity.Configuration
{
    public class RootConfiguration : IRootConfiguration
    {
        public IAdminConfiguration AdminConfiguration { get; set; }
        public IRegisterConfiguration RegisterConfiguration { get; }
        public IBrandingConfiguration BrandingConfiguration { get; }

        public RootConfiguration(IOptions<AdminConfiguration> adminConfiguration, IOptions<RegisterConfiguration> registerConfiguration, IOptions<BrandingConfiguration> brandingConfiguration)
        {
            RegisterConfiguration = registerConfiguration.Value;
            AdminConfiguration = adminConfiguration.Value;
            BrandingConfiguration = brandingConfiguration.Value;
        }
    }
}