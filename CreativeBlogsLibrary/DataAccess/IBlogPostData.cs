namespace CreativeBlogsLibrary.DataAccess;

public interface IBlogPostData
{
    Task BookmarkBlogPost(string blogpostId, string userId);
    Task CreateBlogPost(BlogPostModel blogPost);
    Task<List<BlogPostModel>> GetAllBlogPosts();
    Task<BlogPostModel> GetBlogPost(string id);
    Task UpdateBlogPost(BlogPostModel blogPost);
}