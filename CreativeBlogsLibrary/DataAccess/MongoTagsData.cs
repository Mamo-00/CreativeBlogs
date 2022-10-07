using Microsoft.Extensions.Caching.Memory;

namespace CreativeBlogsLibrary.DataAccess;
public class MongoTagsData : ITagsData
{
	private readonly IMongoCollection<TagModel> tags;
	private readonly IMemoryCache cache;
	private const string CacheName = "TagData";
	public MongoTagsData(IDbConnection db, IMemoryCache cache)
	{
		this.cache = cache;
		this.tags = db.TagCollection;
	}

	public async Task<List<TagModel>> GetAllTags()
	{
		var output = this.cache.Get<List<TagModel>>(CacheName);
		if (output is null)
		{
			var results = await this.tags.FindAsync(_ => true);
			output = results.ToList();

			this.cache.Set(CacheName, output, TimeSpan.FromDays(1));
		}
		return output;
	}

	public Task CreateTags(TagModel tags)
	{
		return this.tags.InsertOneAsync(tags);
	}
}
