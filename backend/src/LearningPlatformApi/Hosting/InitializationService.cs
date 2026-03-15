using LearningPlatformApi.Persistence.Context;
using LearningPlatformApi.Persistence.Entities;
using Microsoft.AspNetCore.Identity;

namespace LearningPlatformApi.Hosting;

public class InitializationService(IServiceScopeFactory scopeFactory,
    IHostEnvironment hostEnvironment, ILogger<InitializationService> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Starting initialization service");
        
        using var scope = scopeFactory.CreateScope();
        var applicationContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

        try
        {
            logger.LogInformation("Ensuring database creation...");
            await applicationContext.Database.EnsureCreatedAsync(stoppingToken);
            logger.LogInformation("Database created");
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Unable to initialize database");
            throw;
        }
        
        if (!hostEnvironment.IsDevelopment())
        {
            logger.LogInformation("Host environment is '{HostEnvironment}', skipping initialization...",
                hostEnvironment.EnvironmentName);
        }

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();

        string[] roles = { "Admin", "Teacher", "Student" };

        logger.LogInformation("Creating base roles [{Roles}]", string.Join(", ", roles));
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        logger.LogInformation("Creating administrator...");
        if (hostEnvironment.IsDevelopment())
        {
            var adminEmail = "admin@learningplatform.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new UserEntity
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }

        logger.LogInformation("Initialization service completed");
    }
}