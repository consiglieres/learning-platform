using LearningPlatformApi.V1.Models.Base;
using LearningPlatformApi.V1.Models.Lessons;

namespace LearningPlatformApi.V1.Models.Module.Res;

public class ModuleDiffDto
{
    public List<LessonDiffDto> AddedLessons { get; set; } = new();
    public List<LessonDiffDto> RemovedLessons { get; set; } = new();
    public List<LessonDiffDto> ModifiedLessons { get; set; } = new();
    public List<ModulePropertyChangesDto> PropertyChanges { get; set; } = new();
}