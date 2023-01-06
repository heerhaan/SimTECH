using Microsoft.AspNetCore.Authorization;
using SimTECH.Data.Models;

namespace SimTECH.Data.Requirements
{
    public class ChoiceRequirementHandler : AuthorizationHandler<ChoiceRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ChoiceRequirement requirement)
        {
            var user = User.FromClaimsPrincipal(context.User);
            var minimumRequired = Convert.ToInt32(context.Resource);

            if (user.CoolGrade >= minimumRequired)
                context.Succeed(requirement);
            else
                context.Fail();

            return Task.CompletedTask;
        }
    }
}
