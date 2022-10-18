using Microsoft.AspNetCore.Components.Authorization;
namespace CreativeBlogs.Components.Helpers;

public static class AuthenticationStateProviderHelpers
{
    public static async Task<UserModel> GetUserFromAuth(this AuthenticationStateProvider authProvider, IUserData userData )
    {
        var authState = await authProvider.GetAuthenticationStateAsync();
        string objectId = authState.User.Claims.FirstOrDefault(c => c.Type.Contains("objectidentifier"))?.Value;
        return await userData.GetUserFromAuthentication(objectId);
    }
}
