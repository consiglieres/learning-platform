namespace LearningPlatformApi.V1.Models.Req.Courses;

public class V1CreateCourseDraftRequest
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required List<V1CourseCategory> Categories { get; set; }
}