namespace LearningPlatformApi.V1.Models.Tasks.Req;

public class V1UpdateCodingTaskReqDto
{
    public string? Name { get; set; }
    public int? Order { get; set; }
    public string? DifficultyName { get; set; }
    public int? DifficultyPoints { get; set; }
    public string? InitialCode { get; set; }
    public string? TestCode { get; set; }
}