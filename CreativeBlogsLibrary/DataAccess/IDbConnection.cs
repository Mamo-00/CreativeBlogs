using MongoDB.Driver;

namespace CreativeBlogsLibrary.DataAccess;
public interface IDbConnection
{
    IMongoCollection<BlogPostModel> BlogPostCollection { get; }
    string BlogPostCollectionName { get; }
    MongoClient Client { get; }
    string DbName { get; }
    IMongoCollection<TagModel> TagCollection { get; }
    string TagCollectionName { get; }
    IMongoCollection<UserModel> UserCollection { get; }
    string UserCollectionName { get; }
}