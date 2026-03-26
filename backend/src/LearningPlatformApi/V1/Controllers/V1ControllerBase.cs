using Microsoft.AspNetCore.Mvc;

namespace LearningPlatformApi.V1.Controllers;

[ApiController]
[Route("api/v1")]
[ApiExplorerSettings(GroupName = "v1")]
public class V1ControllerBase : ControllerBase
{
    /*internal IdentityUser CurrentUser => HttpContext.User.*/
}