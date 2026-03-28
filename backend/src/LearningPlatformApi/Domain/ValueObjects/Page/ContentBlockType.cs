namespace LearningPlatformApi.Domain.ValueObjects.Page;

public record ContentBlockType(ContentCategory Category, ContentType ContentType)
{
    public static ContentBlockType CreateMarkup => new(ContentCategory.Markup, ContentType.Html);

    public static ContentBlockType CreateImage => new(ContentCategory.Image, ContentType.Url);

    public static ContentBlockType CreateUrl => new(ContentCategory.Video, ContentType.Url);

    public static ContentBlockType CreateCode => new(ContentCategory.Code, ContentType.PlainText);
}