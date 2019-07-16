namespace Skoruba.IdentityServer4.STS.Identity.Configuration.Intefaces
{
    public interface IBrandingConfiguration
    {
        bool IsDefined { get; }
        string Footer { get; set; }
        string Name { get; set; }
        string ImageUrl { get; set; }
        bool ShowIcon { get; set; }
        string SubTitle { get; set; }
        string Title { get; set; }
    }
}