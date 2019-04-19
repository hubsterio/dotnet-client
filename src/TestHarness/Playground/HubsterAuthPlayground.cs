using Hubster.Auth;
using Hubster.Auth.Models;
using System.Net;

namespace TestHarness.Playground
{
    public static class HubsterAuthPlayground
    {
        static IdentityResponse<IdentityToken> OnAuthRequest(HubsterAuthClient client)
        {
            var apiResponse = client.GetUserToken("user1@email.com", "Password123");
            return apiResponse;
        }

        static void GetUserToken_Good()
        {
            var authClient = new HubsterAuthClient(OnAuthRequest, "http://localhost:5000");
            var apiResponse = authClient.GetUserToken("user1@email.com", "Password123!");
        }

        static void GetUserToken_BadCreds()
        {
            var authClient = new HubsterAuthClient(OnAuthRequest, "http://localhost:5000");
            var apiResponse = authClient.GetUserToken("unknown", "unknown");
        }

        static void GetUserToken_BadUrl()
        {
            var authClient = new HubsterAuthClient(OnAuthRequest, "http://localhost:500");
            var apiResponse = authClient.GetUserToken("unknown", "unknown");
        }

        static void GetClientToken_Good()
        {
            var authClient = new HubsterAuthClient(OnAuthRequest, "http://localhost:5000");
            var apiResponse = authClient.GetClientToken("hubster.engine.api.00000000000000000000000000000001", "9c5Vbnd0vZGlqTdBzhz9hb9cQ0M=");
        }

        static void GetClientToken_BadCreds()
        {
            var authClient = new HubsterAuthClient(OnAuthRequest, "http://localhost:5000");
            var apiResponse = authClient.GetClientToken("unknown", "unknown");
        }

        static void GetUserToken_BadGrant()
        {
            // need to change the grant_type in the IdentitAccess class to test this.
            var authClient = new HubsterAuthClient(OnAuthRequest, "http://localhost:5000");
            var apiResponse = authClient.GetUserToken("user1@email.com", "Password123!");
        }

        static void GetRefreshToken_Good()
        {
            var authClient = new HubsterAuthClient(OnAuthRequest, "http://localhost:5000");
            var apiResponse = authClient.GetUserToken("user1@email.com", "Password123!");

            apiResponse = authClient.EnsureLifespan(apiResponse.Token);
        }

        static void EnsureLifeSpan_Good()
        {
            var authClient = new HubsterAuthClient(OnAuthRequest, "http://localhost:5000");
            var apiResponse = authClient.EnsureLifespan();

            if (apiResponse.StatusCode == HttpStatusCode.OK)
            {
                apiResponse = authClient.EnsureLifespan(apiResponse.Token);
            }
        }

        public static void Run()
        {
            // GetUserToken_Good();
            // GetUserToken_BadUrl();
            // GetUserToken_BadCreds();
            // GetClientToken_Good();
            // GetClientToken_BadCreds();
            // GetUserToken_BadGrant();
            // GetRefreshToken_Good();
            EnsureLifeSpan_Good();
        }
    }
}
