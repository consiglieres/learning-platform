namespace LearningPlatformApi.V1.Models.Req.Courses;

public class V1UpdateCourseInfoRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public required List<V1CourseCategory>? Categories { get; set; }
}