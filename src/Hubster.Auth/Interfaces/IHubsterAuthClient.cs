using Hubster.Auth.Models;

namespace Hubster.Auth.Interfaces
{
    public interface IHubsterAuthClient
    {
        IdentityResponse<IdentityToken> EnsureLifespan(IdentityToken token = null);
        IdentityResponse<IdentityToken> GetClientToken(string clientId, string secret);
        IdentityResponse<IdentityToken> GetUserToken(string username, string password);
        IdentityResponse<IdentityToken> RefreshToken(IdentityToken token);
    }
}