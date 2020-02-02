using System.Collections.Generic;
using System.Linq;

namespace Skoruba.MultiTenant
{
    public class TenantResult
    {
        public TenantResult() { }
        private TenantResult(bool success, params string[] errors)
        {
            Succeeded = success;
            Errors = errors ?? new string[] { };
        }
        public bool Succeeded { get; protected set; }
        public IEnumerable<string> Errors { get; }
        public static TenantResult Failed(params string[] errors)
        {
            return new TenantResult(false, errors);
        }
        public static TenantResult Success()
        {
            return new TenantResult(true, null);
        }
        public override string ToString()
        {
            var result = Succeeded ?
                "Success" : Errors.Count() == 1 ?
                Errors.First() :
                $"There are {Errors.Count()} errors.";

            return result;
        }
    }

}
