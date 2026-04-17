using LearningPlatformApi.Authorization.AuthorizationHandlers;
using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Domain.Entities.Tasks;
using LearningPlatformApi.Domain.Repositories;
using LearningPlatformApi.Hosting;
using LearningPlatformApi.Mapper;
using LearningPlatformApi.Mapper.Impl;
using LearningPlatformApi.Persistence.Context;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.Entities.Page;
using LearningPlatformApi.Persistence.Repositories;
using LearningPlatformApi.Persistence.Repositories.Base;
using LearningPlatformApi.Services;
using LearningPlatformApi.Services.Impl;
using LearningPlatformApi.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// HostedServices
builder.Services.AddHostedService<InitializationService>();

// Options
builder.Services.AddOptions<EmailSettings>()
    .Bind(configuration.GetSection("Email"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

// Persistence
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("ApplicationContext")));

// Identity - ПОЛНАЯ настройка
builder.Services.AddIdentity<UserEntity, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;

    // Настройки блокировки
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // Настройки пользователя
    options.User.RequireUniqueEmail = true;

    // Настройки входа
    options.SignIn.RequireConfirmedEmail = true;
    options.SignIn.RequireConfirmedPhoneNumber = false;
})
.AddEntityFrameworkStores<ApplicationContext>()
.AddDefaultTokenProviders()
.AddSignInManager()
.AddUserManager<UserManager<UserEntity>>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://yourfrontend.com")
              .AllowCredentials()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
});

// Services
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAuthorizationHandler, ResourceAuthorizationHandler>();

// Mappers
builder.Services.AddSingleton<IUserMapper, UserMapper>();
builder.Services.AddSingleton<IDbEntityMapper<CodingTask, string, CodingTaskEntity, string>, LearningPlatformApi.Mapper.Impl.CodingTaskMapper>();
builder.Services.AddSingleton<IDbEntityMapper<Course, string, CourseEntity, string>, CourseMapper>();
builder.Services.AddSingleton<IDbEntityMapper<Lesson, string, LessonEntity, string>, LessonMapper>();
builder.Services.AddSingleton<IDbEntityMapper<Module, string, ModuleEntity, string>, ModuleMapper>();
builder.Services.AddSingleton<IDbEntityMapper<Page, string, PageEntity, string>, PageMapper>();
builder.Services.AddSingleton<IDbEntityMapper<TestTask, string, TestTaskEntity, string>, TestTaskMapper>();

// Services
builder.Services.AddScoped<IUserRegistrationService, UserRegistrationService>();
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IUserEmailService, UserEmailService>();
builder.Services.AddScoped<ICourseService, CourseService>();

// Repositories
builder.Services.AddScoped<ICourseRepository, CoursesRepository>();

// Authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy =>
        policy.RequireRole("Admin"));

    options.AddPolicy("RequireTeacherRole", policy =>
        policy.RequireRole("Teacher", "Admin"));

    options.AddPolicy("RequireStudentRole", policy =>
        policy.RequireRole("Student", "Teacher", "Admin"));

    options.AddPolicy("EmailConfirmed", policy =>
        policy.RequireClaim("email_confirmed", "True"));
});

// Controllers
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();
app.UseCors("FrontendApp");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
else
{
    app.UseHttpsRedirection();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();