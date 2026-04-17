using LearningPlatformApi.Domain.ValueObjects.Base;

namespace LearningPlatformApi.Domain.ValueObjects;

public class Role : ValueObject
{
    public static readonly Role Owner = new("owner", "Владелец");
    public static readonly Role Teacher = new("teacher", "Преподаватель");
    public static readonly Role Student = new("student", "Студент");

    private Role(string code, string name)
    {
        Code = code;
        Name = name;
    }

    public string Code { get; }

    public string Name { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
    }
}