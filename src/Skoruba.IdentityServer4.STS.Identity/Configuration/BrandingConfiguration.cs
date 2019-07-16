using Skoruba.IdentityServer4.STS.Identity.Configuration.Intefaces;

namespace Skoruba.IdentityServer4.STS.Identity.Configuration
{
    public class BrandingConfiguration : IBrandingConfiguration
    {
        public bool IsDefined => !string.IsNullOrWhiteSpace(Name);
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Footer { get; set; }
        public bool ShowIcon { get; set; }
    }
}