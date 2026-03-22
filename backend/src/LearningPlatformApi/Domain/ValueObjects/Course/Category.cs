using LearningPlatformApi.Domain.ValueObjects.Base;

namespace LearningPlatformApi.Domain.ValueObjects.Course;

public class Category : ValueObject
{
    public static readonly Category Programming = new("programming", "Программирование");
    public static readonly Category DataScience = new("data-science", "Анализ данных");
    public static readonly Category Python = new("python", "Python");
    public static readonly Category JavaScript = new("javascript", "JavaScript");
    public static readonly Category BeginnerEasy = new("beginner", "Easy");

    public string Code { get; }

    public string Name { get; }

    private Category(string code, string name)
    {
        Code = code;
        Name = name;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
    }
}