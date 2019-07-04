namespace Skoruba.IdentityServer4.Admin.Configuration.Interfaces
{
    public interface ITwoFactorAuthenticationConfiguration : EntityFramework.Shared.Validators.ITwoFactorAuthenticationConfiguration
    {
        bool EnableAuthenticatorApp { get; set; }
        bool EnableEmailTotp { get; set; }
        bool EnableSmsTotp { get; set; }

        //bool IsRequired { get; set; } // <-- is defined on inherited interface
    }
}