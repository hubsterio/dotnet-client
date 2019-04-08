using Hubster.Auth;
using Hubster.Auth.Models;
using Hubster.Direct;
using Hubster.Direct.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestHarness.Playground
{
    public static class HubsterDirectPlayground
    {
        static IdentityResponse<IdentityToken> OnAuthorizationRequest(HubsterAuthClient client)
        {
            var apiResponse = client.GetClientToken("hubster.engine.api.00000000000000000000000000000001", "9c5Vbnd0vZGlqTdBzhz9hb9cQ0M=");
            return apiResponse;
        }

        static void EstablishConversation()
        {
            var auth = new HubsterAuthClient(OnAuthorizationRequest, "http://localhost:5000");
            var direct = new HubsterDirectClient(auth, "http://localhost:8251");

            var apiResponse = direct.Conversation.Establish(new EstablishConversationRequestModel
            {
                IntegrationId = "00000000-0000-0000-0000-000000000020",
                Binding = "my unique data",
                Properties = new ConversationPropertiesModel
                {
                    Profile = new Dictionary<string, string>
                    {
                        { "Device", "Web" },
                        { "FullName", "Ross Pellegrino-2" },
                    }
                }
            });
        }

        static void GetEstablishedConversation()
        {
            var auth = new HubsterAuthClient(OnAuthorizationRequest, "http://localhost:5000");
            var direct = new HubsterDirectClient(auth, "http://localhost:8251");

            var apiResponse = direct.Conversation.GetEstablished(Guid.Parse("b33bab55-df33-4a11-ac19-b09c83309c06"));
        }


        public static void Run()
        {
            // EstablishConversation();
            GetEstablishedConversation();
        }
    }
}
