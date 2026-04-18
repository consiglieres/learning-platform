using System.Text.Json.Serialization;

namespace LearningPlatformApi.Domain.ValueObjects.Page;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ContentType
{
    Markdown,
    Html,
    Url,
    PlainText
}