using LearningPlatformApi.Persistence.Entities.Base;

namespace LearningPlatformApi.Persistence.Entities;

public class CodingTaskEntity(string id) : TaskBaseEntity(id)
{
    public string InitialCode { get; set; }

    public string TestCode { get; set; }
}