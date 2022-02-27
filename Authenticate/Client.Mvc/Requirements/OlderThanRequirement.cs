using Microsoft.AspNetCore.Authorization;

namespace Client.Mvc.Requirements
{
    public class OlderThanRequirement : IAuthorizationRequirement
    {
        public readonly int Years;

        public OlderThanRequirement(int years)
        {
            Years = years;
        }
    }
}
