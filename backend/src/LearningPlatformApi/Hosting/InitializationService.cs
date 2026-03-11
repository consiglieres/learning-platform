using LearningPlatformApi.Persistence.Entities;
using Microsoft.AspNetCore.Identity;

namespace LearningPlatformApi.Hosting;

public class InitializationService(IServiceScopeFactory scopeFactory, IHostEnvironment hostEnvironment)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = scopeFactory.CreateScope();
        
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    
        string[] roles = { "Admin", "Teacher", "Student" };
    
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    
        // Создание администратора по умолчанию (только для разработки)
        if (hostEnvironment.IsDevelopment())
        {
            var adminEmail = "admin@learningplatform.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
        
            if (adminUser == null)
            {
                adminUser = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
            
                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}