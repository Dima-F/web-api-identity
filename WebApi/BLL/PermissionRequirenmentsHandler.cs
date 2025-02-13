using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using WebApi.Persistence;
using WebApi.Persistence.Models;

namespace WebApi.BLL;

public class PermissionRequirenmentsHandler(IServiceScopeFactory serviceScopeFactory): AuthorizationHandler<PermissionRequirements>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirements requirement)
    {
        // ClaimTypes.Email - зазначений в JwtService
        var emailClaim = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
        if (emailClaim == null) return Task.CompletedTask;
        {
            // напряму не можна зареквайрити скоуп, приходиться створювати тут
            using var scope = serviceScopeFactory.CreateScope();
            var accountRepository = scope.ServiceProvider.GetService<AccountRepository>();
            var permission = accountRepository?.GetPermissionByEmail(emailClaim.Value);
            if (permission != null && permission.Any(x => x.Name == requirement.Permission))
            {
                context.Succeed(requirement);
            }
        }
        return Task.CompletedTask;
    }
}