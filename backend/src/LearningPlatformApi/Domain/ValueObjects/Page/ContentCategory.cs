using System.Text.Json.Serialization;

namespace LearningPlatformApi.Domain.ValueObjects.Page;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ContentCategory
{
    Markup,
    Image,
    Video,
    Code
}