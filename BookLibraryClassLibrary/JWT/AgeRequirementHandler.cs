using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.JWT
{
    public class AgeRequirementHandler : AuthorizationHandler<AgeRequirment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgeRequirment requirement)
        {
            if (!context.User.HasClaim(claim => claim.Type == "age"))
            {
                return Task.CompletedTask;
            }

            if (!int.TryParse(context.User.FindFirst(c => c.Type == "age").Value, out int actualAge))
            {
                return Task.CompletedTask;
            }

            if (actualAge >= requirement.Age)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
