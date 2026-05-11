using LearningPlatformApi.Mapper;
using LearningPlatformApi.Services;
using LearningPlatformApi.V1.Models.Page;
using LearningPlatformApi.V1.Models.Page.Req;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatformApi.V1.Controllers;

[Route("api/v1/pages")]
[ApiController]
public class V1PageController(IPageService pageService, IUserMapper userMapper, IUserProfileService profileService)
    : ControllerBase
{
    // GET: api/v1/pages/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<V1PageResDto>> GetPage(string id,
        CancellationToken cancellationToken = default)
    {
        var result = await pageService.GetByIdAsync(id, cancellationToken);

        return Ok(result);
    }

    // POST: api/v1/pages
    [HttpPost]
    public async Task<ActionResult<V1PageResDto>> CreatePage([FromBody] CreatePageRequest request,
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
    
    // DELETE: api/v1/pages/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePage(string id, CancellationToken cancellationToken = default)
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