using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Mapper;
using LearningPlatformApi.Services;
using LearningPlatformApi.Services.DataObjects.Request.Course;
using LearningPlatformApi.V1.Models.Req;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatformApi.V1.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class V1CoursesController(
    ICourseService courseService,
    IUserMapper userMapper,
    IUserProfileService profileService)
    : ControllerBase
{
    /// <summary>
    /// Создание черновика курса
    /// </summary>
    [HttpPost("draft")]
    [Authorize(Roles = "Teacher,Admin")]
    public async Task<IActionResult> CreateCourseDraftAsync([FromBody] V1CreateCourseDraftRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var dbUser = await profileService.GetCurrentUserAsync(User);
        if (dbUser == null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "User Not Found",
                Status = StatusCodes.Status404NotFound
            });
        }
        var user = userMapper.MapToDomain(dbUser);
        
        // Преобразуем категории из запроса в TypedCategory
        var categories = MapToTypedCategories(request.Categories);
        
        var createRequest = new CreateCourseDraftRequest(
            request.Title,
            request.Description,
            categories,
            user);

        var result = await courseService.CreateCourseDraftAsync(createRequest);

        return result.Match<IActionResult>(
            error => BadRequest(new ProblemDetails
            {
                Title = "Course Creation Failed",
                Detail = $"{error.OperationInfo.Code}:{error.OperationInfo.Description}",
                Status = StatusCodes.Status400BadRequest
            }),
            success => Ok(new
            {
                courseId = success.Value.Id,
                message = "Course draft created successfully"
            })
        );
    }

    /// <summary>
    /// Получение последней версии курса
    /// </summary>
    [HttpGet("{courseId}/last")]
    public async Task<IActionResult> GetCourseLastAsync(string courseId)
    {
        var result = await courseService.GetCourseLastAsync(courseId);

        return result.Match<IActionResult>(
            notExists => NotFound(new ProblemDetails
            {
                Title = "Course Not Found",
                Detail = $"Course with id {notExists.EntityId} not found",
                Status = StatusCodes.Status404NotFound
            }),
            success => Ok(success.Value)
        );
    }

    /// <summary>
    /// Получение конкретной версии курса
    /// </summary>
    [HttpGet("{courseId}/version/{version}")]
    public async Task<IActionResult> GetCourseVersionAsync(string courseId, int version)
    {
        var result = await courseService.GetCourseVersionAsync(courseId, version);

        return result.Match<IActionResult>(
            notExists => NotFound(new ProblemDetails
            {
                Title = "Course Not Found",
                Detail = $"Course with id {notExists.EntityId} not found",
                Status = StatusCodes.Status404NotFound
            }),
            success => Ok(success.Value)
        );
    }

    /// <summary>
    /// Обновление информации о курсе
    /// </summary>
    [HttpPut("{courseId}")]
    [Authorize(Roles = "Teacher,Admin")]
    public async Task<IActionResult> UpdateCourseInfoAsync(string courseId, [FromBody] V1UpdateCourseInfoRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updateRequest = new UpdateCourseInfoRequest(
            request.Title,
            request.Description,
            MapToTypedCategories(request.Categories));

        var result = await courseService.UpdateCourseInfoAsync(courseId, updateRequest);

        return result.Match<IActionResult>(
            notFound => NotFound(new ProblemDetails
            {
                Title = "Course Not Found",
                Detail = $"Course with id {courseId} not found",
                Status = StatusCodes.Status404NotFound
            }),
            error => BadRequest(new ProblemDetails
            {
                Title = "Update Failed",
                Detail = $"{error.OperationInfo.Code}:{error.OperationInfo.Description}",
                Status = StatusCodes.Status400BadRequest
            }),
            success => Ok(new
            {
                course = success.Value,
                message = "Course updated successfully"
            })
        );
    }

    /// <summary>
    /// Удаление курса (черновика)
    /// </summary>
    [HttpDelete("{courseId}")]
    [Authorize(Roles = "Teacher,Admin")]
    public async Task<IActionResult> DeleteCourseAsync(string courseId, CancellationToken cancellationToken)
    {
        var entityUser = await profileService.GetCurrentUserAsync(User);
        if (entityUser == null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "User Not Found",
                Status = StatusCodes.Status404NotFound
            });
        }
        var user = userMapper.MapToDomain(entityUser);
        var result = await courseService.DeleteCourseAsync(courseId, user, cancellationToken);

        return result.Match<IActionResult>(
            notFound => NotFound(new ProblemDetails
            {
                Title = "Course Not Found",
                Detail = $"Course with id {courseId} not found",
                Status = StatusCodes.Status404NotFound
            }),
            success => Ok(new { message = "Course deleted successfully" })
        );
    }

    /// <summary>
    /// Отправка курса на модерацию
    /// </summary>
    [HttpPost("{courseId}/submit")]
    [Authorize(Roles = "Teacher,Admin")]
    public async Task<IActionResult> SubmitForModerationAsync(string courseId)
    {
        var entityUser = await profileService.GetCurrentUserAsync(User);
        if (entityUser == null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "User Not Found",
                Status = StatusCodes.Status404NotFound
            });
        }
        var user = userMapper.MapToDomain(entityUser);
        var result = await courseService.SubmitForModerationCourseAsync(courseId, user);

        return result.Match<IActionResult>(
            notFound => NotFound(new ProblemDetails
            {
                Title = "Course Not Found",
                Detail = $"Course with id {courseId} not found",
                Status = StatusCodes.Status404NotFound
            }),
            validationFailed => BadRequest(new ProblemDetails
            {
                Title = "Submission Failed",
                Detail = validationFailed.ErrorMessage,
                Status = StatusCodes.Status400BadRequest
            }),
            success => Ok(new { message = "Course submitted for moderation successfully" })
        );
    }

    /// <summary>
    /// Одобрение курса модератором (публикация)
    /// </summary>
    [HttpPost("{courseId}/approve")]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> ApproveCourseAsync(string courseId, [FromBody] V1ModerationCommentRequest? request = null)
    {
        var entityUser = await profileService.GetCurrentUserAsync(User);
        if (entityUser == null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "User Not Found",
                Status = StatusCodes.Status404NotFound
            });
        }
        var user = userMapper.MapToDomain(entityUser);
        var comment = request != null 
            ? new ModerationCourseComment(request.Comment) 
            : null;
            
        var result = await courseService.ApprovePublishCourseAsync(courseId, user, comment);

        return result.Match<IActionResult>(
            notFound => NotFound(new ProblemDetails
            {
                Title = "Course Not Found",
                Detail = $"Course with id {courseId} not found",
                Status = StatusCodes.Status404NotFound
            }),
            validationFailed => BadRequest(new ProblemDetails
            {
                Title = "Approval Failed",
                Detail = validationFailed.ErrorMessage,
                Status = StatusCodes.Status400BadRequest
            }),
            success => Ok(new { message = "Course approved and published successfully" })
        );
    }

    /// <summary>
    /// Отклонение курса модератором
    /// </summary>
    [HttpPost("{courseId}/reject")]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> RejectCourseAsync(string courseId, [FromBody] V1ModerationCommentRequest request)
    {
        if (string.IsNullOrEmpty(request?.Comment))
            return BadRequest(new { error = "Rejection comment is required" });

        var entityUser = await profileService.GetCurrentUserAsync(User);
        if (entityUser == null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "User Not Found",
                Status = StatusCodes.Status404NotFound
            });
        }
        var user = userMapper.MapToDomain(entityUser);
        var comment = new ModerationCourseComment(request.Comment);
        var result = await courseService.RejectCourseAsync(courseId, user, comment);

        return result.Match<IActionResult>(
            notFound => NotFound(new ProblemDetails
            {
                Title = "Course Not Found",
                Detail = $"Course with id {courseId} not found",
                Status = StatusCodes.Status404NotFound
            }),
            validationFailed => BadRequest(new ProblemDetails
            {
                Title = "Rejection Failed",
                Detail = validationFailed.ErrorMessage,
                Status = StatusCodes.Status400BadRequest
            }),
            success => Ok(new { message = "Course rejected successfully" })
        );
    }

    /// <summary>
    /// Снятие курса с публикации (возврат в черновик)
    /// </summary>
    [HttpPost("{courseId}/unpublish")]
    [Authorize(Roles = "Teacher,Admin")]
    public async Task<IActionResult> UnpublishCourseAsync(string courseId)
    {
        var entityUser = await profileService.GetCurrentUserAsync(User);
        if (entityUser == null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "User Not Found",
                Status = StatusCodes.Status404NotFound
            });
        }
        var user = userMapper.MapToDomain(entityUser);
        var result = await courseService.UnpublishCourseAsync(courseId, user);

        return result.Match<IActionResult>(
            notFound => NotFound(new ProblemDetails
            {
                Title = "Course Not Found",
                Detail = $"Course with id {courseId} not found",
                Status = StatusCodes.Status404NotFound
            }),
            validationFailed => BadRequest(new ProblemDetails
            {
                Title = "Unpublish Failed",
                Detail = validationFailed.ErrorMessage,
                Status = StatusCodes.Status400BadRequest
            }),
            success => Ok(new { message = "Course unpublished successfully" })
        );
    }

    /// <summary>
    /// Архивирование курса
    /// </summary>
    [HttpPost("{courseId}/archive")]
    [Authorize(Roles = "Teacher,Admin")]
    public async Task<IActionResult> ArchiveCourseAsync(string courseId)
    {
        var entityUser = await profileService.GetCurrentUserAsync(User);
        if (entityUser == null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "User Not Found",
                Status = StatusCodes.Status404NotFound
            });
        }
        var user = userMapper.MapToDomain(entityUser);
        var result = await courseService.ArchiveCourseAsync(courseId, user);

        return result.Match<IActionResult>(
            notFound => NotFound(new ProblemDetails
            {
                Title = "Course Not Found",
                Detail = $"Course with id {courseId} not found",
                Status = StatusCodes.Status404NotFound
            }),
            validationFailed => BadRequest(new ProblemDetails
            {
                Title = "Archive Failed",
                Detail = validationFailed.ErrorMessage,
                Status = StatusCodes.Status400BadRequest
            }),
            success => Ok(new { message = "Course archived successfully" })
        );
    }

    /// <summary>
    /// Восстановление курса из архива
    /// </summary>
    [HttpPost("{courseId}/restore")]
    [Authorize(Roles = "Teacher,Admin")]
    public async Task<IActionResult> RestoreCourseFromArchiveAsync(string courseId)
    {
        var entityUser = await profileService.GetCurrentUserAsync(User);
        if (entityUser == null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "User Not Found",
                Status = StatusCodes.Status404NotFound
            });
        }
        var user = userMapper.MapToDomain(entityUser);
        var result = await courseService.RestoreCourseFromArchiveAsync(courseId, user);

        return result.Match<IActionResult>(
            notFound => NotFound(new ProblemDetails
            {
                Title = "Course Not Found",
                Detail = $"Course with id {courseId} not found",
                Status = StatusCodes.Status404NotFound
            }),
            validationFailed => BadRequest(new ProblemDetails
            {
                Title = "Restore Failed",
                Detail = validationFailed.ErrorMessage,
                Status = StatusCodes.Status400BadRequest
            }),
            success => Ok(new { message = "Course restored from archive successfully" })
        );
    }
    
    private IReadOnlyCollection<TypedCategory> MapToTypedCategories(List<V1CourseCategory> requestCategories)
    {
        return requestCategories.Select(x 
            => new TypedCategory(x.TypeName, x.ValueName)).ToList();
    }
}