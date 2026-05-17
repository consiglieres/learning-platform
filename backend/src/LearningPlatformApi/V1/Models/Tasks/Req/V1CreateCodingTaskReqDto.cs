namespace LearningPlatformApi.V1.Models.Tasks.Req;

public class V1CreateCodingTaskReqDto
{
    public required string Name { get; set; }
    public required int Order { get; set; }
    public required string DifficultyName { get; set; }
    public required int DifficultyPoints { get; set; }
    public required string InitialCode { get; set; }
    public required string TestCode { get; set; }
}