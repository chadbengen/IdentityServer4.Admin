### Visual Studio command line (Nuget package manager):

#### Migrations for Asp.Net Core Identity DbContext:

```powershell
Add-Migration AspNetIdentityDbInit -context AdminIdentityDbContext -output Data/Migrations/Identity
Update-Database -context AdminIdentityDbContext
```
#### Migrations for Asp.Net Core MultiTenant Identity DbContext:

```powershell
Add-Migration AspNetMultiTenantIdentityDbInit -context MultiTenantUserIdentityDbContext -output Data/Migrations/Identity
Update-Database -context MultiTenantUserIdentityDbContext
```

#### Migrations for Logging DbContext:

```powershell
Add-Migration LoggingDbInit -context AdminLogDbContext -output Data/Migrations/Logging
Update-Database -context AdminLogDbContext
```

#### Migrations for IdentityServer configuration DbContext:

```powershell
Add-Migration IdentityServerConfigurationDbInit -context IdentityServerConfigurationDbContext -output Data/Migrations/IdentityServerConfiguration
Update-Database -context IdentityServerConfigurationDbContext
```

#### Migrations for IdentityServer persisted grants DbContext:

```powershell
Add-Migration IdentityServerPersistedGrantsDbInit -context IdentityServerPersistedGrantDbContext -output Data/Migrations/IdentityServerGrants
Update-Database -context IdentityServerPersistedGrantDbContext
```

### Or via `dotnet CLI`:

#### Migrations for Asp.Net Core Identity DbContext:

```powershell
dotnet ef migrations add AspNetIdentityDbInit -c AdminIdentityDbContext -o Data/Migrations/Identity
dotnet ef database update -c AdminIdentityDbContext
```

#### Migrations for Logging DbContext:

```powershell
dotnet ef migrations add LoggingDbInit -c AdminLogDbContext -o Data/Migrations/Logging
dotnet ef database update -c AdminLogDbContext
```

#### Migrations for IdentityServer configuration DbContext:

```powershell
dotnet ef migrations add IdentityServerConfigurationDbInit -c IdentityServerConfigurationDbContext -o Data/Migrations/IdentityServerConfiguration
dotnet ef database update -c IdentityServerConfigurationDbContext
```

#### Migrations for IdentityServer persisted grants DbContext:

```powershell
dotnet ef migrations add IdentityServerPersistedGrantsDbInit -c IdentityServerPersistedGrantDbContext -o Data/Migrations/IdentityServerGrants
dotnet ef database update -c IdentityServerPersistedGrantDbContext
```