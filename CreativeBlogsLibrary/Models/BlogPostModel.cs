namespace CreativeBlogsLibrary.Models;

public class BlogPostModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public BasicUserModel Author { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    public TagModel Tag { get; set; }

    public HashSet<string> Bookmarks { get; set; } = new();
}
