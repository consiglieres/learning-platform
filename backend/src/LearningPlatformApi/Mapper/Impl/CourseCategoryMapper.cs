using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Persistence.Entities;
using Riok.Mapperly.Abstractions;

namespace LearningPlatformApi.Mapper.Impl;

[Mapper]
internal sealed partial class CourseCategoryMapper : ICourseCategoryMapper
{
    [MapProperty(nameof(CategoryEntity.TypeName), "Type")]
    [MapProperty(nameof(CategoryEntity.ValueName), "Value")]
    [MapperRequiredMapping(RequiredMappingStrategy.Target)]
    public partial TypedCategory Map(CategoryEntity categoryEntity);

    [MapProperty(nameof(TypedCategory.Type), nameof(CategoryEntity.TypeName))]
    [MapProperty(nameof(TypedCategory.Value), nameof(CategoryEntity.ValueName))]
    public partial CategoryEntity Map(TypedCategory categoryEntity);
}