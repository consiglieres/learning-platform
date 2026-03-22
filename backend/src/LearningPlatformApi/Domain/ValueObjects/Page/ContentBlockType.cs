namespace LearningPlatformApi.Domain.ValueObjects.Page;

public record ContentBlockType
{
    public ContentCategory Category { get; }

    public ContentType ContentType { get; }

    public string Data { get; }

    private ContentBlockType(ContentCategory category, ContentType contentType, string data)
    {
        Category = category;
        ContentType = contentType;
        Data = data;
    }

    public static ContentBlockType CreateMarkup(string data)
    {
        return new ContentBlockType(ContentCategory.Markup, ContentType.Html, data);
    }

    public static ContentBlockType CreateImage(string data)
    {
        return new ContentBlockType(ContentCategory.Image, ContentType.Url, data);
    }

    public static ContentBlockType CreateUrl(string data)
    {
        return new ContentBlockType(ContentCategory.Video, ContentType.Url, data);
    }

    public static ContentBlockType CreateCode(string data)
    {
        return new ContentBlockType(ContentCategory.Code, ContentType.PlainText, data);
    }
}