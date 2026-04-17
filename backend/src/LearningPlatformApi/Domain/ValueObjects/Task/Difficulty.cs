using LearningPlatformApi.Domain.Exceptions;

namespace LearningPlatformApi.Domain.ValueObjects.Task;

public record Difficulty(string Name, int BasePoints)
{
    public static readonly Difficulty Easy = new("Easy", 10);

    public static readonly Difficulty Medium = new("Medium", 20);

    public static readonly Difficulty Hard = new("Hard", 30);

    public static Difficulty FromString(string name)
    {
        return name switch
        {
            "Easy" => Easy,
            "Medium" => Medium,
            "Hard" => Hard,
            _ => throw new DomainException($"Unknown difficulty: {name}")
        };
    }

    public Difficulty NextHigher()
    {
        return this switch
        {
            _ when this == Easy => Medium,
            _ when this == Medium => Hard,
            _ => Hard
        };
    }

    public Difficulty NextLower()
    {
        return this switch
        {
            _ when this == Hard => Medium,
            _ when this == Medium => Easy,
            _ => Easy
        };
    }
}