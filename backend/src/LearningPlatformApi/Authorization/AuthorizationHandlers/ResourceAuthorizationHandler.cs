using System.Security.Claims;
using LearningPlatformApi.Authorization.Requirement;
using LearningPlatformApi.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformApi.Authorization.AuthorizationHandlers;

public class ResourceAuthorizationHandler : AuthorizationHandler<ResourceOwnerRequirement, Guid>
{
    private readonly ApplicationContext applicationContext;

    public ResourceAuthorizationHandler(ApplicationContext applicationContext)
    {
        this.applicationContext = applicationContext;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ResourceOwnerRequirement requirement,
        Guid resourceId)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (string.IsNullOrEmpty(userId))
            return;

        var resource = await applicationContext.Resources.FindAsync(resourceId);
        if (resource != null && resource.OwnerId.ToString() == userId)
        {
            context.Succeed(requirement);
            return;
        }

        var userResource = await applicationContext.UserResources
            .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.ResourceId == resourceId);
        
        if (userResource != null && userResource.Permission == "Manage")
        {
            context.Succeed(requirement);
            return;
        }
        
        if (context.User.IsInRole("Admin"))
        {
            context.Succeed(requirement);
        }
    }
}