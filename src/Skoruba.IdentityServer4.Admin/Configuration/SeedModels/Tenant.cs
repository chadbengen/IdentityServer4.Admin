namespace Skoruba.IdentityServer4.Admin.Configuration.SeedModels
{
    public class Tenant
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string CareCompleteDbName { get; set; }
    }
}