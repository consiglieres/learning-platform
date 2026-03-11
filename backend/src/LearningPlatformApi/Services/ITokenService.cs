using System.Security.Claims;
using LearningPlatformApi.Persistence.Entities;

namespace LearningPlatformApi.Services;

public interface ITokenService
{
    Task<string> GenerateAccessTokenAsync(AppUser user);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}