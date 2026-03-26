namespace LearningPlatformApi.Domain.ValueObjects.Course;

public record CategoryType
{
    public static readonly CategoryType Direction = new("direction", "Направление");
    public static readonly CategoryType Technology = new("technology", "Технология");
    public static readonly CategoryType Difficulty = new("difficulty", "Сложность курса");

    public string Code { get; }
    public string Name { get; }

    private CategoryType(string code, string name)
    {
        Code = code;
        Name = name;
    }
};