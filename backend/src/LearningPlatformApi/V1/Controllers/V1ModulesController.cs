using JetBrains.Annotations;
using LearningPlatformApi.Mapper;
using LearningPlatformApi.Services;
using LearningPlatformApi.V1.Models.Module.Req;
using LearningPlatformApi.V1.Models.Module.Res;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatformApi.V1.Controllers;

[Route("api/v1/modules")]
[ApiController]
public class V1ModulesController(
    IUserMapper userMapper,
    IModuleService moduleService,
    IUserProfileService profileService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<V1ModuleResDto>> GetModule(string id, CancellationToken cancellationToken = default)
    {
        var result = await moduleService.GetByIdAsync(id, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<V1ModuleResDto>> CreateModule([FromBody] CreateModuleRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var dbUser = await profileService.GetCurrentUserAsync(User);
        if (dbUser == null)
            return NotFound(new ProblemDetails
            {
                Title = "User Not Found",
                Status = StatusCodes.Status404NotFound
            });
        var user = userMapper.MapToDomain(dbUser);

        var result = await moduleService.CreateAsync(request, user, cancellationToken);
        return CreatedAtAction(nameof(GetModule), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<V1ModuleResDto>> UpdateModule(string id, [FromBody] UpdateModuleRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var dbUser = await profileService.GetCurrentUserAsync(User);
        if (dbUser == null)
            return NotFound(new ProblemDetails
            {
                Title = "User Not Found",
                Status = StatusCodes.Status404NotFound
            });
        var user = userMapper.MapToDomain(dbUser);

        var result = await moduleService.UpdateAsync(id, user, request, cancellationToken);
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteModule(string id, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var dbUser = await profileService.GetCurrentUserAsync(User);
        if (dbUser == null)
            return NotFound(new ProblemDetails
            {
                Title = "User Not Found",
                Status = StatusCodes.Status404NotFound
            });
        var user = userMapper.MapToDomain(dbUser);

        await moduleService.DeleteAsync(id, user, cancellationToken);
        return NoContent();
    }

    [HttpPost("{id}/restore")]
    public async Task<ActionResult<V1ModuleResDto>> RestoreModule(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var dbUser = await profileService.GetCurrentUserAsync(User);
        if (dbUser == null)
            return NotFound(new ProblemDetails
            {
                Title = "User Not Found",
                Status = StatusCodes.Status404NotFound
            });
        var user = userMapper.MapToDomain(dbUser);

        var result = await moduleService.RestoreAsync(id, user, cancellationToken);
        return Ok(result);
    }
}

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class RollbackModuleRequest
{
    public int TargetVersionOrder { get; set; }
    public string? Reason { get; set; }
}