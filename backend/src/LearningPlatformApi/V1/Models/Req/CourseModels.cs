namespace LearningPlatformApi.V1.Models.Req;

public class V1CreateCourseDraftRequest
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<string>? Categories { get; set; }
}

public class V1UpdateCourseInfoRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public List<string>? Categories { get; set; }
}

public class V1ModerationCommentRequest
{
    public string Comment { get; set; } = null!;
}