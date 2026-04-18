namespace LearningPlatformApi.V1.Models.Page.Req;

public class CopyPageRequest
{
    public required string SourcePageId { get; set; }
    public int? SourceVersionOrder { get; set; }
    public int NewOrder { get; set; }
}