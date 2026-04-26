using LearningPlatformApi.V1.Models.Page.Res;

namespace LearningPlatformApi.V1.Models.Base;

public class ModulePropertyChangesDto
{
    public required PropertyChangeDto<string> Name { get; set; }

    public required PropertyChangeDto<int> ModuleOrder { get; set; }

    public required PropertyChangeDto<PageDiffDto> PageDiff { get; set; }
}

public class PropertyChangeDto<T>
{
    public required string PropertyName { get; set; }

    public required T NewValue { get; set; }

    public required T OldValue { get; set; }
}