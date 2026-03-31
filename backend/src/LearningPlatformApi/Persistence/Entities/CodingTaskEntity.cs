using LearningPlatformApi.Persistence.Entities.Base;
using LearningPlatformApi.Persistence.Entities.Page;

namespace LearningPlatformApi.Persistence.Entities;

public class CodingTaskEntity(string id) : TaskBaseEntity(id)
{
    public string InitialCode { get; set; }

    public string TestCode { get; set; }
}