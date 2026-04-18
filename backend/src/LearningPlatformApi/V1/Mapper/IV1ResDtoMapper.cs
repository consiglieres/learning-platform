using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.Entities.Tasks;
using LearningPlatformApi.V1.Models.Account.Res;
using LearningPlatformApi.V1.Models.Courses.Res;
using LearningPlatformApi.V1.Models.Lessons;
using LearningPlatformApi.V1.Models.Module;
using LearningPlatformApi.V1.Models.Tasks;

namespace LearningPlatformApi.V1.Mapper;

public interface IV1ResDtoMapper
{
    V1UserResDto Map(User user);
    
    V1Course Map(Course course);
    
    V1ModuleResDto Map(Module module);
    
    V1LessonResDto Map(Lesson lesson);
    
    V1TaskShortInfo MapShort(CodingTask task);
    
    V1TaskShortInfo MapShort(TestTask task);
    
    V1TestTaskResDto Map(TestTask task);
    
    V1CodingTaskResDto Map(CodingTask task);
}