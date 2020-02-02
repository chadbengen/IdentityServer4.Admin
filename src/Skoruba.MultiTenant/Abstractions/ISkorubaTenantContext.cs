using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Skoruba.MultiTenant.Abstractions
{
    public interface ISkorubaTenantContext
    {
        ISkorubaTenant Tenant { get; }
        bool MultiTenantEnabled { get; }
        bool TenantResolved { get; }
        bool TenantResolutionRequired { get; }
        string TenantResolutionStrategy { get; }
    }
}
