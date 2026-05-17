namespace LearningPlatformApi.V1.Models.Tasks.Req;

public class V1UpdateTestTaskReqDto
{
    public string? Name { get; set; }
    public int? Order { get; set; }
    public string? DifficultyName { get; set; }
    public int? DifficultyPoints { get; set; }
    public string? Question { get; set; }
    public IReadOnlyList<string>? Options { get; set; }
    public IReadOnlyList<string>? Answers { get; set; }
}