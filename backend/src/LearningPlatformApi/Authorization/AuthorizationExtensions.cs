using LearningPlatformApi.Authorization.Requirement;

namespace LearningPlatformApi.Authorization;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireAdminRole", policy =>
                policy.RequireRole("Admin"));
            
            options.AddPolicy("RequireTeacherRole", policy =>
                policy.RequireRole("Teacher", "Admin"));
            
            options.AddPolicy("RequireStudentRole", policy =>
                policy.RequireRole("Student", "Teacher", "Admin"));

            options.AddPolicy("EmailConfirmed", policy =>
                policy.RequireClaim("email_confirmed", "True"));

            options.AddPolicy("TeacherOrAdmin", policy =>
                policy.RequireAssertion(context =>
                    context.User.HasClaim(c => c.Type == "email_confirmed" && c.Value == "True") &&
                    (context.User.IsInRole("Teacher") || context.User.IsInRole("Admin"))
                ));

            options.AddPolicy("ResourceOwner", policy =>
                policy.Requirements.Add(new ResourceOwnerRequirement()));
        });

        return services;
    }
}
