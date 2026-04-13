namespace LearningPlatformApi.Domain.ValueObjects.Page;

public record PageType
{
    public static readonly PageType Introduction = new("intro", "Введение");

    public static readonly PageType Theory = new("theory", "Теория");

    public static readonly PageType Task = new("task", "Задание");

    public string Code { get; }
    public string Name { get; }

    public PageType(string code, string name)
    {
        Code = code;
        Name = name;
    }
}