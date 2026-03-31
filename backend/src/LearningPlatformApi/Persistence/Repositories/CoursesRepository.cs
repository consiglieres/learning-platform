using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Mapper;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.Repositories.Base.Impl;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformApi.Persistence.Repositories;

public class CoursesRepository : PublicationWorkflowRepository<Course, string, CourseEntity, string>
{
    public CoursesRepository(DbContext context, IDbEntityMapper<Course, string, CourseEntity, string> mapper,
        ILogger<CoursesRepository> logger) : base(context, mapper, logger)
    {
    }
}