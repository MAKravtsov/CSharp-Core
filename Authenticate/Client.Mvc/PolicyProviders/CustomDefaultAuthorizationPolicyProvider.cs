using Client.Mvc.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Client.Mvc.PolicyProviders
{
    public class CustomDefaultAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private readonly AuthorizationOptions _options;

        public CustomDefaultAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
            _options = options.Value;
        }

        public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            var policyExists = await base.GetPolicyAsync(policyName);

            if (policyExists == null)
            {
                policyExists = new AuthorizationPolicyBuilder()
                    .AddRequirements(new OlderThanRequirement(10))
                    .Build();

                _options.AddPolicy(policyName, policyExists);
            }

            return policyExists;
        }
    }
}
