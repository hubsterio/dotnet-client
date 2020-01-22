using Hubster.Auth;
using Hubster.Auth.Models;
using Hubster.Direct;
using Hubster.Abstractions.Enums;
using Hubster.Abstractions.Models;
using Hubster.Abstractions.Models.Direct;
using System;
using System.Collections.Generic;
using System.Net;

namespace TestHarness.Playground
{
    public static class HubsterDirectPlayground
    {
        static IdentityResponse<IdentityToken> OnAuthorizationRequest(HubsterAuthClient client)
        {
            var apiResponse = client.GetClientToken("hubster.engine.api.00000000000000000000000000000001", "9c5Vbnd0vZGlqTdBzhz9hb9cQ0M=");
            return apiResponse;
        }

        static ApiResponse<EstablishedConversationModel> EstablishConversation(HubsterAuthorizer authorizer)
        {
            var direct = new HubsterDirectClientCustomer("http://localhost:8251");

            //var apiResponse = direct.Conversation.Establish(authorizer, new EstablishConversationRequestModel
            //{
            //    IntegrationId = "00000000-0000-0000-0000-000000000001",
            //    Binding = "my unique data",
            //    Properties = new ConversationPropertiesModel
            //    {
            //        Profile = new Dictionary<string, string>
            //        {
            //            { "Device", "Web" },
            //            { "Full name", "Ross Pellegrino-2" },
            //        }
            //    }
            //});

            // return apiResponse;
            return null;
        }

        static ApiResponse<EstablishedConversationModel> GetEstablishedConversation(HubsterAuthorizer authorizer)
        {
            var direct = new HubsterDirectClientCustomer("http://localhost:8251");
            // var apiResponse = direct.Conversation.GetEstablished(authorizer, Guid.Parse("71E202DD-F67D-4245-B1FD-2A558332AE90"));

            // return apiResponse;
            return null;
        }

        static void GetActivities(HubsterAuthorizer authorizer)
        {
            var direct = new HubsterDirectClientCustomer("http://localhost:8251");
            var conResponse = GetEstablishedConversation(authorizer);
            if (conResponse.StatusCode == HttpStatusCode.OK)
            {
                var actResponse = direct.Activity.Get(authorizer, conResponse.Content, 0);
            }
        }

        static void SendActivityToAgent(HubsterAuthorizer authorizer)
        {
            var direct = new HubsterDirectClientCustomer("http://localhost:8251");

            var conResponse = GetEstablishedConversation(authorizer);

            if (conResponse.StatusCode == HttpStatusCode.OK)
            {
                var actResponse = direct.Activity.Send(authorizer, conResponse.Content, new DirectActivityModel
                {
                    Message = new DirectMessageModel
                    {
                        Text = "Hello from Customer TestPlayGround "
                    },
                });
            }
        }

        static void SendActivityToCustomer(HubsterAuthorizer authorizer)
        {
            var direct = new HubsterDirectClientBusiness("http://localhost:8251");

            var conResponse = GetEstablishedConversation(authorizer);
            if (conResponse.StatusCode == HttpStatusCode.OK)
            {
                var actResponse = direct.Activity.Send(authorizer, conResponse.Content.ConversationId.Value, new DirectActivityModel
                {
                    Sender = new DirectSourceModel
                    {
                        IntegrationId = "00000000-0000-0000-0000-000000000002",
                    },
                    Message = new DirectMessageModel
                    {
                        Text = "Hello from Business TestPlayGround "
                    },
                });
            }
        }


        public static void Run()
        {
            var auth = new HubsterAuthClient(OnAuthorizationRequest, "http://localhost:5000");
            var authorizer = new HubsterAuthorizer(auth);

            // EstablishConversation(authorizer);
            // GetEstablishedConversation(authorizer);
            // GetActivities(authorizer);
            // SendActivityToAgent(authorizer);
            SendActivityToCustomer(authorizer);
        }
    }
}
