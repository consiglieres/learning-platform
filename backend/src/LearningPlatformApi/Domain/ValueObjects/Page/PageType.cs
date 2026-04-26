namespace LearningPlatformApi.Domain.ValueObjects.Page;

public record PageType
{
    public static readonly PageType Introduction = new("intro", "Введение");

    public static readonly PageType Theory = new("theory", "Теория");

    public static readonly PageType Task = new("task", "Задание");

    public PageType(string code, string name)
    {
        Code = code;
        Name = name;
    }

    public string Code { get; set; }
    public string Name { get; set; }
}