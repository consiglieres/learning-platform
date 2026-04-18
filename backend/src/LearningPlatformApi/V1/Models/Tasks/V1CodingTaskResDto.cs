using LearningPlatformApi.Domain.ValueObjects.Task;
using LearningPlatformApi.V1.Models.Base;
using LearningPlatformApi.V1.Models.Page;

namespace LearningPlatformApi.V1.Models.Tasks;

public class V1CodingTaskResDto : VersionableResDto
{
    public required string Name { get; set; }
    
    public int Order { get; set; }
    
    public required Difficulty Difficulty { get; set; }

    public required string LessonId { get; set; }

    public required V1PageResDto PageContent { get; set; }
    
    public required string InitialCode { get; set; }
}