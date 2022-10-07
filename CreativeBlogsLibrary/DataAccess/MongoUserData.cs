namespace CreativeBlogsLibrary.DataAccess;

public class MongoUserData : IUserData
{
	IMongoCollection<UserModel> users;

	public MongoUserData(IDbConnection db)
	{
		this.users = db.UserCollection;
	}

	public async Task<List<UserModel>> GetUsersAsync()
	{
		var results = await this.users.FindAsync(_ => true);
		return results.ToList();
	}

	public async Task<UserModel> GetUser(string id)
	{
		var results = await this.users.FindAsync(u => u.Id == id);
		return results.FirstOrDefault();
	}

	public async Task<UserModel> GetUserFromAuthentication(string objectId)
	{
		var results = await this.users.FindAsync(u => u.ObjectIdentifier == objectId);
		return results.FirstOrDefault();
	}

	public Task CreateUser(UserModel user)
	{
		return this.users.InsertOneAsync(user);
	}

	public Task UpdateUser(UserModel user)
	{
		var filter = Builders<UserModel>.Filter.Eq("Id", user.Id);
		return this.users.ReplaceOneAsync(filter, user, new ReplaceOptions { IsUpsert = true });
	}
}

