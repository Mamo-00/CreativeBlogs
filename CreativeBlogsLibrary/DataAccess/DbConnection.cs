using Microsoft.Extensions.Configuration;


namespace CreativeBlogsLibrary.DataAccess;

public class DbConnection : IDbConnection
{
    private readonly IConfiguration configuration;
    private readonly IMongoDatabase db;
    private string connectionId = "MongoDB";

    public string DbName { get; private set; }
    public string TagCollectionName { get; private set; } = "tags";
    public string BlogPostCollectionName { get; private set; } = "blogposts";
    public string UserCollectionName { get; private set; } = "user";

    public MongoClient Client { get; private set; }

    public IMongoCollection<TagModel> TagCollection { get; private set; }
    public IMongoCollection<BlogPostModel> BlogPostCollection { get; private set; }
    public IMongoCollection<UserModel> UserCollection { get; private set; }


    public DbConnection(IConfiguration configuration)
    {
        this.configuration = configuration;
        Client = new MongoClient(this.configuration.GetConnectionString(connectionId));
        DbName = this.configuration["DatabaseName"];
        this.db = Client.GetDatabase(DbName);

        TagCollection = this.db.GetCollection<TagModel>(TagCollectionName);
        BlogPostCollection = this.db.GetCollection<BlogPostModel>(BlogPostCollectionName);
        UserCollection = this.db.GetCollection<UserModel>(UserCollectionName);
    }
}
