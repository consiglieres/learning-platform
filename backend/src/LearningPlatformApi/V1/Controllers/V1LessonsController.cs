using LearningPlatformApi.Mapper;
using LearningPlatformApi.Services;
using LearningPlatformApi.V1.Models.Lessons.Req;
using LearningPlatformApi.V1.Models.Lessons.Res;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatformApi.V1.Controllers;

[Route("api/v1/lessons")]
[ApiController]
public class V1LessonsController(
    IUserMapper userMapper,
    ILessonService lessonService,
    IUserProfileService profileService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<V1LessonResDto>> GetLessonAsync(
        string id,
        [FromQuery] int? versionOrder = null,
        CancellationToken cancellationToken = default)
    {
        var result = versionOrder.HasValue
            ? await lessonService.GetByVersionAsync(id, versionOrder.Value, cancellationToken)
            : await lessonService.GetLatestAsync(id, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id}/latest")]
    public async Task<ActionResult<V1LessonResDto>> GetLatestLessonAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var result = await lessonService.GetLatestAsync(id, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}/history")]
    public async Task<ActionResult<List<V1LessonVersionInfoResDto>>> GetLessonHistoryAsync(
        string id,
        [FromQuery] int limit = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await lessonService.GetVersionHistoryAsync(id, limit, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<V1LessonResDto>> CreateLessonAsync([FromBody] V1CreateLessonReqDto request,
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

        var result = await lessonService.CreateAsync(request, user, cancellationToken);
        return CreatedAtAction(nameof(GetLessonAsync), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<V1LessonResDto>> UpdateLessonAsync(
        string id,
        [FromBody] V1UpdateLessonReqDto request,
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

        var result = await lessonService.UpdateAsync(id, user, request, cancellationToken);
        return Ok(result);
    }

    [HttpPost("{id}/rollback")]
    public async Task<ActionResult<V1LessonResDto>> RollbackLessonAsync(
        string id,
        [FromBody] RollbackModuleRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await lessonService.RollbackToVersionAsync(
            id,
            request.TargetVersionOrder,
            request.Reason,
            cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLessonAsync(string id, CancellationToken cancellationToken = default)
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

        await lessonService.DeleteAsync(id, user, cancellationToken);
        return NoContent();
    }

    [HttpPost("{id}/restore")]
    public async Task<ActionResult<V1LessonResDto>> RestoreLessonAsync(
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

        var result = await lessonService.RestoreAsync(id, user, cancellationToken);
        return Ok(result);
    }
}