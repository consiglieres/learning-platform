namespace LearningPlatformApi.V1.Models.Tasks.Req;

public class V1CreateTestTaskReqDto
{
    public required string Name { get; set; }
    public required int Order { get; set; }
    public required string DifficultyName { get; set; }
    public required int DifficultyPoints { get; set; }
    public required string Question { get; set; }
    public required IReadOnlyList<string> Options { get; set; }
    public required IReadOnlyList<string> Answers { get; set; }
}