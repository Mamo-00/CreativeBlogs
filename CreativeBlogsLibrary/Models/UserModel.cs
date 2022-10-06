namespace CreativeBlogsLibrary.Models;

public class UserModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string ObjectIdentifier { get; set; }

    public string DisplayName { get; set; }

    public string EmailAddress { get; set; }

    public List<BlogPostModel> AuthoredPosts { get; set; } = new();

    public List<BlogPostModel> BookmarkedPosts { get; set; } = new();

}
