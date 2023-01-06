using Microsoft.AspNetCore.Authorization;
using SimTECH.Data.Models;

namespace SimTECH.Data.Requirements
{
    public class CoolRequirementHandler : AuthorizationHandler<CoolRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CoolRequirement requirement)
        {
            var user = User.FromClaimsPrincipal(context.User);

            if (user.CoolGrade >= requirement.MinimumCoolGradeToBeCool)
                context.Succeed(requirement);
            else
                context.Fail();

            return Task.CompletedTask;
        }
    }
}
