using Hubster.Auth;

namespace TestHarness.Playground
{
    public static class HubsterAuthPlayground
    {
        public static void GetUserToken_Good()
        {
            var authClient = new HubsterAuthClient("http://localhost:5000");
            var response = authClient.GetUserToken("user1@email.com", "Password123!");
        }

        public static void GetUserToken_BadCreds()
        {
            var authClient = new HubsterAuthClient("http://localhost:5000");
            var response = authClient.GetUserToken("unknown", "unknown");
        }

        public static void GetUserToken_BadUrl()
        {
            var authClient = new HubsterAuthClient("http://localhost:500");
            var response = authClient.GetUserToken("unknown", "unknown");
        }

        public static void GetClientToken_Good()
        {
            var authClient = new HubsterAuthClient("http://localhost:5000");
            var response = authClient.GetClientToken("hubster.engine.api.00000000000000000000000000000001", "9c5Vbnd0vZGlqTdBzhz9hb9cQ0M=");
        }

        public static void GetClientToken_BadCreds()
        {
            var authClient = new HubsterAuthClient("http://localhost:5000");
            var response = authClient.GetClientToken("unknown", "unknown");
        }

        public static void GetUserToken_BadGrant()
        {
            // need to change the grant_type in the IdentitAccess class to test this.
            var authClient = new HubsterAuthClient("http://localhost:5000");
            var response = authClient.GetUserToken("user1@email.com", "Password123!");
        }

        public static void GetRefreshToken_Good()
        {
            var authClient = new HubsterAuthClient("http://localhost:5000");
            var response = authClient.GetUserToken("user1@email.com", "Password123!");

            response = authClient.EnsureLifespand(response.Token);
        }

        public static void Run()
        {
            // GetUserToken_Good();
            GetUserToken_BadUrl();
            // GetUserToken_BadCreds();
            // GetClientToken_Good();
            // GetClientToken_BadCreds();
            // GetUserToken_BadGrant();
            // GetRefreshToken_Good();
        }
    }
}
