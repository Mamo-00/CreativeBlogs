namespace CreativeBlogsLibrary.Models;
public class BasicBlogPostModel
{


    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Title { get; set; }

    public BasicBlogPostModel()
    {

    }

    public BasicBlogPostModel(BlogPostModel blogPost)
    {
        Id = blogPost.Id;
        Title = blogPost.Title;
    }

}
