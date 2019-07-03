﻿using System.Collections.Generic;

namespace Skoruba.IdentityServer4.Admin.Configuration.SeedModels
{
    public class User
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string TenantId { get; set; }
        public int CareCompleteId { get; set; }
        public List<Claim> Claims { get; set; } = new List<Claim>();
        public List<string> Roles { get; set; } = new List<string>();
    }
}