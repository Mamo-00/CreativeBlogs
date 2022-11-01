using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

namespace CreativeBlogsUI;

public static class RegisterServices
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor().AddMicrosoftIdentityConsentHandler();
        builder.Services.AddMemoryCache();
        builder.Services.AddControllersWithViews().AddMicrosoftIdentityUI();
        
        builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"));
        builder.Services.AddSingleton<IDbConnection, DbConnection>();
        builder.Services.AddSingleton<ITagsData, MongoTagsData>();
        builder.Services.AddSingleton<IBlogPostData, MongoBlogPostData>();
        builder.Services.AddSingleton<IUserData, MongoUserData>();
    }
}
