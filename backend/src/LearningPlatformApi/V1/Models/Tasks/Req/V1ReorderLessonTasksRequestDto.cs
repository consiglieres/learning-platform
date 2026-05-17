namespace LearningPlatformApi.V1.Models.Tasks.Req;

public class V1ReorderLessonTasksRequestDto
{
    public required IReadOnlyCollection<string> TasksOrderIds { get; set; }
}