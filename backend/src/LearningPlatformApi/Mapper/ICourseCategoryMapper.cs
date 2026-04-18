using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Persistence.Entities;

namespace LearningPlatformApi.Mapper;

public interface ICourseCategoryMapper
{
    TypedCategory Map(CategoryEntity categoryEntity);
    
    CategoryEntity Map(TypedCategory categoryEntity);
}