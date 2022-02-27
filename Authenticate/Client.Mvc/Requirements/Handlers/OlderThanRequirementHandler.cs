using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using System.Threading.Tasks;
using System;
using System.Security.Claims;

namespace Client.Mvc.Requirements.Handlers
{
    public class OlderThanRequirementHandler : AuthorizationHandler<OlderThanRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OlderThanRequirement requirement)
        {
            var hasClaim = context.User.HasClaim(y => y.Type == ClaimTypes.DateOfBirth);
            if (!hasClaim)
            {
                return Task.CompletedTask;
            }

            var dateOfBirth = context.User.FindFirst(y => y.Type == ClaimTypes.DateOfBirth).Value;

            var date = DateTime.Parse(dateOfBirth, CultureInfo.CurrentCulture);

            var year = DateTime.Today.Year;

            var canEnterDiff = (year - date.Year);

            if (canEnterDiff > requirement.Years)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
