
namespace LearningPlatformApi.Domain.Entities.Courses;

public record TypedCategory(string Type, string Value)
{
    public static readonly TypedCategory Programming = new("Направление", "Программирование");
    public static readonly TypedCategory DataScience = new( "Направление", "Анализ данных");
    public static readonly TypedCategory Python = new("Технология", "Python");
    public static readonly TypedCategory JavaScript = new("Технология", "JavaScript");
    public static readonly TypedCategory BeginnerEasy = new("Сложность курса", "Easy");
}
