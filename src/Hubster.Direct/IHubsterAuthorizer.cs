using Hubster.Auth;
using Hubster.Auth.Models;
using Hubster.Direct.Models;

namespace Hubster.Direct
{
    public interface IHubsterAuthorizer
    {
        IHubsterAuthClient AuthClient { get; }
        IdentityToken Token { get; }

        bool EnsureLifespan(ApiResponse apiResponse);
    }
}