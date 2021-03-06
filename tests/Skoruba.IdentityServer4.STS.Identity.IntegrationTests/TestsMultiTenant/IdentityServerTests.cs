﻿using System.Threading.Tasks;
using FluentAssertions;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc.Testing;
using Skoruba.IdentityServer4.STS.Identity.Configuration.Test;
using Skoruba.IdentityServer4.STS.Identity.IntegrationTests.TestsMultiTenant.Base;
using Xunit;

namespace Skoruba.IdentityServer4.STS.Identity.IntegrationTests.TestsMultiTenant
{
    public class IdentityServerTests : BaseClassFixture
    {
        public IdentityServerTests(WebApplicationFactory<StartupTestMultiTenant> factory) : base(factory)
        {
        }

        [Fact]
        public async Task CanShowDiscoveryEndpoint()
        {
            var disco = await Client.GetDiscoveryDocumentAsync("http://localhost");

            disco.Should().NotBeNull();
            disco.IsError.Should().Be(false);

            disco.KeySet.Keys.Count.Should().Be(1);
        }
    }
}
