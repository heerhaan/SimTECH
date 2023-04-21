using Microsoft.AspNetCore.Authorization;
using SimTECH.Data.Models;

namespace SimTECH.Data.Requirements
{
    // Authentication & authorization has been disabled for now
    public class CoolRequirement : IAuthorizationRequirement
    {
        public int MinimumCoolGradeToBeCool { get; set; } = 8;
    }

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
