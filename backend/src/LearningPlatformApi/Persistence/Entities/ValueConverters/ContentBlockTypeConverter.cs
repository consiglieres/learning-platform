
using LearningPlatformApi.Domain.ValueObjects.Page;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LearningPlatformApi.Persistence.Entities.ValueConverters;

internal sealed class ContentBlockTypeConverter : ValueConverter<ContentBlockType, string>
{
    public ContentBlockTypeConverter() 
        : base(
            v => ConvertToString(v),
            v => ConvertToObject(v)
        )
    {
    }
    
    private static string ConvertToString(ContentBlockType blockType)
    {
        return $"{blockType.Category}:{blockType.ContentType}";
    }
    
    private static ContentBlockType ConvertToObject(string value)
    {
        var parts = value.Split(':', 2);
        
        if (parts.Length != 2 || !Enum.TryParse<ContentCategory>(parts[0], out var category)
                              || !Enum.TryParse<ContentType>(parts[1], out var contentType))
        {
            throw new ArgumentException($"Invalid ContentBlockType format: {value}");
        }
        
        return new ContentBlockType(category, contentType);
    }
}