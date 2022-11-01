namespace CreativeBlogsLibrary.DataAccess;

public interface IBlogPostData
{
    Task BookmarkBlogPost(string blogpostId, string userId);
    Task CreateBlogPost(BlogPostModel blogPost);
    Task<List<BlogPostModel>> GetAllBlogPosts();
    Task<BlogPostModel> GetBlogPost(string id);
    Task<List<BlogPostModel>> GetUsersBlogPosts(string userId);
    Task UpdateBlogPost(BlogPostModel blogPost);
}