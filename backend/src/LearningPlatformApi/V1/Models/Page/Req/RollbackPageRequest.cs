namespace LearningPlatformApi.V1.Models.Page.Req;

public class RollbackPageRequest
{
    public required int TargetVersionOrder { get; set; }
    public string? Reason { get; set; }
}