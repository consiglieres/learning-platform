using LearningPlatformApi.Mapper;
using LearningPlatformApi.Services;
using LearningPlatformApi.V1.Models.Page;
using LearningPlatformApi.V1.Models.Page.Req;
using LearningPlatformApi.V1.Models.Page.Res;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatformApi.V1.Controllers;

[Route("api/v1/pages")]
[ApiController]
public class V1PageController(
    IPageService pageService,
    IUserMapper userMapper,
    IUserProfileService profileService) : ControllerBase
{
    // GET: api/v1/pages/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<V1PageResDto>> GetPage(
        string id,
        [FromQuery] int? versionOrder = null,
        CancellationToken cancellationToken = default)
    {
        var result = versionOrder.HasValue
            ? await pageService.GetByVersionAsync(id, versionOrder.Value, cancellationToken)
            : await pageService.GetLatestAsync(id, cancellationToken);

        return Ok(result);
    }

    // GET: api/v1/pages/{id}/history
    [HttpGet("{id}/history")]
    public async Task<ActionResult<List<PageVersionInfoDto>>> GetPageHistory(
        string id,
        [FromQuery] int? limit = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await pageService.GetVersionHistoryAsync(id, limit ?? 10, cancellationToken);
        return Ok(result);
    }

    // GET: api/v1/pages/{id}/compare
    [HttpGet("{id}/compare")]
    public async Task<ActionResult<PageComparisonResDto>> CompareVersions(
        string id,
        [FromQuery] int sourceVersion,
        [FromQuery] int targetVersion,
        CancellationToken cancellationToken = default)
    {
        var result = await pageService.CompareVersionsAsync(id, sourceVersion, targetVersion, cancellationToken);
        return Ok(result);
    }

    // POST: api/v1/pages
    [HttpPost]
    public async Task<ActionResult<V1PageResDto>> CreatePage(
        [FromBody] CreatePageRequest request,
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

        var result = await pageService.CreateAsync(request, user, cancellationToken);
        return CreatedAtAction(nameof(GetPage), new { id = result.Id }, result);
    }

    // PUT: api/v1/pages/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<V1PageResDto>> UpdatePage(
        string id,
        [FromBody] UpdatePageRequest request,
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

        var result = await pageService.UpdateAsync(id, user, request, cancellationToken);
        return Ok(result);
    }

    // POST: api/v1/pages/{id}/rollback
    [HttpPost("{id}/rollback")]
    public async Task<ActionResult<V1PageResDto>> RollbackPage(
        string id,
        [FromBody] RollbackPageRequest request,
        CancellationToken cancellationToken = default)
    {
        var result =
            await pageService.RollbackToVersionAsync(id, request.TargetVersionOrder, request.Reason, cancellationToken);
        return Ok(result);
    }

    // POST: api/v1/pages/{id}/copy
    [HttpPost("{id}/copy")]
    public async Task<ActionResult<V1PageResDto>> CopyPage(
        string id,
        [FromBody] CopyPageRequest request,
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

        var result = await pageService.CopyPageAsync(request, user, cancellationToken);
        return CreatedAtAction(nameof(GetPage), new { id = result.Id }, result);
    }

    // DELETE: api/v1/pages/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePage(
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

        await pageService.DeleteAsync(id, user, cancellationToken);
        return NoContent();
    }

    // POST: api/v1/pages/{id}/restore
    [HttpPost("{id}/restore")]
    public async Task<ActionResult<V1PageResDto>> RestorePage(
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
        
        var result = await pageService.RestoreAsync(id, user, cancellationToken);
        return Ok(result);
    }
}