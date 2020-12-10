using ifood_back.Dto;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ifood_back.Policies
{
    public class OnlyExpensiveMastreAuthorizationHandler : AuthorizationHandler<OnlyExpensiveMastreRequirement, UsuarioDto>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OnlyExpensiveMastreRequirement requirement, UsuarioDto resource)
        {
            var UsuarioId  = context.User.Claims.Where(c => c.Type == ClaimTypes.GivenName).Select(c => c.Value).SingleOrDefault();

            if (resource != null && resource.id.ToString() == UsuarioId.ToString())
            {
                context.Succeed(requirement);                
            }
            return Task.CompletedTask;
        }
    }
}
