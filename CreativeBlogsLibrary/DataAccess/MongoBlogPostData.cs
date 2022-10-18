using Microsoft.Extensions.Caching.Memory;

namespace CreativeBlogsLibrary.DataAccess;
public class MongoBlogPostData : IBlogPostData
{
	private readonly IDbConnection db;
	private readonly IUserData userData;
	private readonly IMemoryCache cache;
	private readonly IMongoCollection<BlogPostModel> blogposts;
	private const string CacheName = "BlogPostData";
	public MongoBlogPostData(IDbConnection db, IUserData userData, IMemoryCache cache)
	{
		this.db = db;
		this.userData = userData;
		this.cache = cache;
		blogposts = db.BlogPostCollection;
	}

	public async Task<List<BlogPostModel>> GetAllBlogPosts()
	{
		var output = this.cache.Get<List<BlogPostModel>>(CacheName);
		if (output is null)
		{
			var results = await blogposts.FindAsync(_ => true);
			output = results.ToList();

			this.cache.Set(CacheName, output, TimeSpan.FromMinutes(1));
		}

		return output;
	}

	public async Task<List<BlogPostModel>> GetUsersBlogPosts(string userId)
	{
		var output = cache.Get<List<BlogPostModel>>(userId);
		if (output is null)
		{
			var results = await blogposts.FindAsync(b => b.Author.Id == userId);
			output = results.ToList();

			cache.Set(userId, output, TimeSpan.FromMinutes(1));
		}
		return output;
	}

	public async Task<BlogPostModel> GetBlogPost(string id)
	{
		var results = await blogposts.FindAsync(b => b.Id == id);
		return results.FirstOrDefault();
	}

	public async Task UpdateBlogPost(BlogPostModel blogPost)
	{
		await blogposts.ReplaceOneAsync(b => b.Id == blogPost.Id, blogPost);
		cache.Remove(CacheName);
	}

	public async Task BookmarkBlogPost(string blogpostId, string userId)
	{
		var client = db.Client;
		using var session = await client.StartSessionAsync();

		session.StartTransaction();

		try
		{
			var db = client.GetDatabase(this.db.DbName);
			var blogpostInTransaction = db.GetCollection<BlogPostModel>(this.db.BlogPostCollectionName);
			var blogpost = (await blogpostInTransaction.FindAsync(b => b.Id == blogpostId)).First();

			bool isBookmarked = blogpost.Bookmarks.Add(userId);
			if (isBookmarked == false)
			{
				blogpost.Bookmarks.Remove(userId);
			}

			await blogpostInTransaction.ReplaceOneAsync(session, b => b.Id == blogpostId, blogpost);

			var usersInTransation = db.GetCollection<UserModel>(this.db.UserCollectionName);
			var user = await this.userData.GetUser(blogpost.Author.Id);

			if (isBookmarked)
			{
				user.BookmarkedPosts.Add(new BasicBlogPostModel(blogpost));
			}
			else
			{
				var suggestionToRemove = user.BookmarkedPosts.Where(b => b.Id == blogpostId).First();
				user.BookmarkedPosts.Remove(suggestionToRemove);
			}
			await usersInTransation.ReplaceOneAsync(session, u => u.Id == userId, user);

			await session.CommitTransactionAsync();

			cache.Remove(CacheName);

			//removing cache everytime can be ineffiecient if a lot of bookmarking is happening,
			//therefore I might try to find a better method for bookmarking
		}
		catch (Exception)
		{
			await session.AbortTransactionAsync();
			throw;
		}
	}

	public async Task CreateBlogPost(BlogPostModel blogPost)
	{
		var client = db.Client;
		using var session = await client.StartSessionAsync();
		session.StartTransaction();
		//not using transaction because it is not working for some reason,
		//but would've ideally used it if it worked :(
		try
		{
			var db = client.GetDatabase(this.db.DbName);
			var blogpostInTransaction = db.GetCollection<BlogPostModel>(this.db.BlogPostCollectionName);
			await blogpostInTransaction.InsertOneAsync(session, blogPost);

			var usersInTransaction = db.GetCollection<UserModel>(this.db.UserCollectionName);
			var user = await userData.GetUser(blogPost.Author.Id);
			user.AuthoredPosts.Add(new BasicBlogPostModel(blogPost));
			await usersInTransaction.ReplaceOneAsync(session, u => u.Id == user.Id, user);

			cache.Remove(CacheName);
		}
		catch (Exception ex)
		{
			// TODO: use serilog to log exceptions
			string exception = ex.Message;
			Console.WriteLine(exception);
			await session.AbortTransactionAsync();
			throw;
		}
	}
}
