namespace CreativeBlogsLibrary.DataAccess;

public class MongoUserData
{
	IMongoCollection<UserModel> users;

	public MongoUserData(IDbConnection db)
	{
		this.users = db.UserCollection;
	}


}
