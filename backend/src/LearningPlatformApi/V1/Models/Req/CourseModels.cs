namespace LearningPlatformApi.V1.Models.Req;

public class V1CreateCourseDraftRequest
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required List<V1CourseCategory> Categories { get; set; }
}

public class V1UpdateCourseInfoRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public required List<V1CourseCategory>? Categories { get; set; }
}

public class V1ModerationCommentRequest
{
    public string Comment { get; set; } = null!;
}

public class V1CourseCategory
{
    public required string TypeName { get; set; }
    
    public required string ValueName { get; set; }
}