namespace Skoruba.IdentityServer4.STS.Identity.Configuration.Intefaces
{
    public interface IRootConfiguration
    {
        IAdminConfiguration AdminConfiguration { get; }

        IRegisterConfiguration RegisterConfiguration { get; }
        IBrandingConfiguration BrandingConfiguration { get; }
    }
}