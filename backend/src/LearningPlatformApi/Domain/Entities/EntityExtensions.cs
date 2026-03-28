namespace LearningPlatformApi.Domain.Entities;

public static class EntityExtensions
{
    public static (string TypeCode, string ValueCode) ParseId(this string id)
    {
        var parts = id.Split(':', 2);
        if (parts.Length != 2)
        {
            throw new InvalidOperationException("Invalid id");
        }
        
        return (parts[0], parts[1]);
    }
}