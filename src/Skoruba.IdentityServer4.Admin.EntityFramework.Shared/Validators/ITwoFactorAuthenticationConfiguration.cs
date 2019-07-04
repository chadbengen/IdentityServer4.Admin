namespace Skoruba.IdentityServer4.Admin.EntityFramework.Shared.Validators
{
    public interface ITwoFactorAuthenticationConfiguration
    {
        bool IsRequired { get; set; }
    }
}