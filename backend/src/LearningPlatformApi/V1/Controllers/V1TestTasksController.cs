using LearningPlatformApi.Mapper;
using LearningPlatformApi.Services;
using LearningPlatformApi.V1.Models.Tasks;
using LearningPlatformApi.V1.Models.Tasks.Req;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatformApi.V1.Controllers;

[Route("api/v1/lessons/{lessonId}/tasks")]
[ApiController]
public class V1TasksController(
    IUserMapper userMapper,
    ITaskService taskService,
    IUserProfileService profileService) : ControllerBase
{
    [HttpGet(Name = "GetTasksAsync")]
    public async Task<ActionResult<IReadOnlyCollection<V1TaskShortInfo>>> GetTasksAsync(
        string lessonId,
        CancellationToken cancellationToken = default)
    {
        var result = await taskService.GetTasksAsync(lessonId, cancellationToken);
        
        return result.Match<ActionResult<IReadOnlyCollection<V1TaskShortInfo>>>(
            notFound => NotFound(),
            tasks => Ok(tasks)
        );
    }

    [HttpGet("coding", Name = "GetCodingTasksAsync")]
    public async Task<ActionResult<IReadOnlyCollection<V1CodingTaskResDto>>> GetCodingTasksAsync(
        string lessonId,
        CancellationToken cancellationToken = default)
    {
        var result = await taskService.GetCodingTasksAsync(lessonId, cancellationToken);
        
        return result.Match<ActionResult<IReadOnlyCollection<V1CodingTaskResDto>>>(
            notFound => NotFound(),
            tasks => Ok(tasks)
        );
    }

    [HttpGet("test", Name = "GetTestTasksAsync")]
    public async Task<ActionResult<IReadOnlyCollection<V1TestTaskResDto>>> GetTestTasksAsync(
        string lessonId,
        CancellationToken cancellationToken = default)
    {
        var result = await taskService.GetTestTasksAsync(lessonId, cancellationToken);
        
        return result.Match<ActionResult<IReadOnlyCollection<V1TestTaskResDto>>>(
            notFound => NotFound(),
            tasks => Ok(tasks)
        );
    }

    [HttpGet("coding/{taskId}/execute", Name = "GetCodingTaskForExecutionAsync")]
    public async Task<ActionResult<V1CodingTaskResDto>> GetCodingTaskForExecutionAsync(
        string lessonId,
        string taskId,
        CancellationToken cancellationToken = default)
    {
        var result = await taskService.GetCodingTaskForExecutionAsync(taskId, cancellationToken);
        
        return result.Match<ActionResult<V1CodingTaskResDto>>(
            notFound => NotFound(),
            task => Ok(task)
        );
    }

    [HttpGet("test/{taskId}/execute", Name = "GetTestTaskForExecutionAsync")]
    public async Task<ActionResult<V1TestTaskResDto>> GetTestTaskForExecutionAsync(
        string lessonId,
        string taskId,
        CancellationToken cancellationToken = default)
    {
        var result = await taskService.GetTestTaskForExecutionAsync(taskId, cancellationToken);
        
        return result.Match<ActionResult<V1TestTaskResDto>>(
            notFound => NotFound(),
            task => Ok(task)
        );
    }

    [HttpPost("coding", Name = "AddCodingTaskAsync")]
    public async Task<ActionResult<V1CodingTaskResDto>> AddCodingTaskAsync(
        string lessonId,
        [FromBody] V1CreateCodingTaskReqDto request,
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

        var result = await taskService.AddCodingTaskAsync(lessonId, user, request, cancellationToken);
        
        return result.Match<ActionResult<V1CodingTaskResDto>>(
            notFound => NotFound(),
            validationFailed => BadRequest(validationFailed),
            task => CreatedAtRoute("GetCodingTaskForExecutionAsync", new { lessonId, taskId = task.Id }, task)
        );
    }

    [HttpPost("test", Name = "AddTestTaskAsync")]
    public async Task<ActionResult<V1TestTaskResDto>> AddTestTaskAsync(
        string lessonId,
        [FromBody] V1CreateTestTaskReqDto request,
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

        var result = await taskService.AddTestTaskAsync(lessonId, user, request, cancellationToken);
        
        return result.Match<ActionResult<V1TestTaskResDto>>(
            notFound => NotFound(),
            validationFailed => BadRequest(validationFailed),
            task => CreatedAtRoute("GetTestTaskForExecutionAsync", new { lessonId, taskId = task.Id }, task)
        );
    }

    [HttpPut("coding/{taskId}", Name = "UpdateCodingTaskAsync")]
    public async Task<ActionResult<V1CodingTaskResDto>> UpdateCodingTaskAsync(
        string lessonId,
        string taskId,
        [FromBody] V1UpdateCodingTaskReqDto request,
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

        var result = await taskService.UpdateCodingTaskAsync(taskId, request, user, cancellationToken);
        
        return result.Match<ActionResult<V1CodingTaskResDto>>(
            task => Ok(task)
        );
    }

    [HttpPut("test/{taskId}", Name = "UpdateTestTaskAsync")]
    public async Task<ActionResult<V1TestTaskResDto>> UpdateTestTaskAsync(
        string lessonId,
        string taskId,
        [FromBody] V1UpdateTestTaskReqDto request,
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

        var result = await taskService.UpdateTestTaskAsync(taskId, request, user, cancellationToken);
        
        return result.Match<ActionResult<V1TestTaskResDto>>(
            task => Ok(task)
        );
    }

    [HttpDelete("{taskId}", Name = "DeleteTaskAsync")]
    public async Task<IActionResult> DeleteTaskAsync(
        string lessonId,
        string taskId,
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

        var result = await taskService.DeleteTaskAsync(taskId, user, cancellationToken);
        
        return result.Match<IActionResult>(
            validationFailed => BadRequest(validationFailed),
            success => NoContent()
        );
    }

    [HttpPost("reorder", Name = "ReorderTasksAsync")]
    public async Task<IActionResult> ReorderTasksAsync(
        string lessonId,
        [FromBody] V1ReorderLessonTasksRequestDto request,
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

        var result = await taskService.ReorderTasksAsync(lessonId, user, request, cancellationToken);
        
        return result.Match<IActionResult>(
            notFound => NotFound(),
            validationFailed => BadRequest(validationFailed),
            success => Ok(success)
        );
    }
}