using Microsoft.AspNetCore.Authorization;

namespace SimTECH.Data.Requirements
{
    public class CoolRequirement : IAuthorizationRequirement
    {
        public int MinimumCoolGradeToBeCool { get; set; } = 8;
    }
}
