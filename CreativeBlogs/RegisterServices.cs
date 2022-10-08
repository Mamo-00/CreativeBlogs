namespace CreativeBlogsUI;

public static class RegisterServices
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddMemoryCache();
        

        builder.Services.AddSingleton<IDbConnection, DbConnection>();
        builder.Services.AddSingleton<ITagsData, MongoTagsData>();
        builder.Services.AddSingleton<IBlogPostData, MongoBlogPostData>();
        builder.Services.AddSingleton<IUserData, MongoUserData>();
    }
}
