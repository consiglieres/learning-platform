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
    public async Task<ActionResult<V1ModuleResDto>> GetModule(
        string id,
        [FromQuery] int? versionOrder = null,
        CancellationToken cancellationToken = default)
    {
        var result = versionOrder.HasValue
            ? await moduleService.GetByVersionAsync(id, versionOrder.Value, cancellationToken)
            : await moduleService.GetLatestAsync(id, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id}/latest")]
    public async Task<ActionResult<V1ModuleResDto>> GetLatestModule(
        string id,
        CancellationToken cancellationToken = default)
    {
        var result = await moduleService.GetLatestAsync(id, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}/history")]
    public async Task<ActionResult<List<ModuleVersionInfoDto>>> GetModuleHistory(
        string id,
        [FromQuery] int limit = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await moduleService.GetVersionHistoryAsync(id, limit, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}/compare")]
    public async Task<ActionResult<ModuleComparisonResDto>> CompareVersions(
        string id,
        [FromQuery] int sourceVersion,
        [FromQuery] int targetVersion,
        CancellationToken cancellationToken = default)
    {
        var result = await moduleService.CompareVersionsAsync(id, sourceVersion, targetVersion, cancellationToken);
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
    public async Task<ActionResult<V1ModuleResDto>> UpdateModule(
        string id,
        [FromBody] UpdateModuleRequest request,
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

    [HttpPost("{id}/rollback")]
    public async Task<ActionResult<V1ModuleResDto>> RollbackModule(
        string id,
        [FromBody] RollbackModuleRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await moduleService.RollbackToVersionAsync(
            id,
            request.TargetVersionOrder,
            request.Reason,
            cancellationToken);
        return Ok(result);
    }

    // this endpoint openapi says have some kind of shit
    /*[HttpPost("/copy/{id}")]
    public async Task<ActionResult<V1ModuleResDto>> CopyModule(
        [FromBody] CopyModuleRequest request,
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

        var result = await moduleService.CopyModuleAsync(request, user, cancellationToken);
        return Ok(result);
    }*/

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

public class RollbackModuleRequest
{
    public int TargetVersionOrder { get; set; }
    public string? Reason { get; set; }
}