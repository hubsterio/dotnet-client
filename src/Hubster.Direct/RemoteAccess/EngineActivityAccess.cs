using Hubster.Direct.Enums;
using Hubster.Direct.Models;
using Hubster.Direct.Models.Direct;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace Hubster.Direct.RemoteAccess
{
    /// <summary>
    /// 
    /// </summary>
    internal class EngineActivityAccess : EngineBaseAccess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EngineActivityAccess" /> class.
        /// </summary>
        /// <param name="hostUrl">The host URL.</param>
        public EngineActivityAccess(string hostUrl) : base(hostUrl)
        {
        }

        /// <summary>
        /// Establishes the conversation.
        /// </summary>
        /// <param name="authorizer">The authorizer.</param>
        /// <param name="conversation">The conversation.</param>
        /// <param name="lastEventId">The last event identifier.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public ApiResponse<IEnumerable<DirectActivityModel>> Get(IHubsterAuthorizer authorizer, EstablishedConversationModel conversation, long lastEventId, IntegrationType type)
        {
            var apiResponse = new ApiResponse<IEnumerable<DirectActivityModel>>();
            if(authorizer.EnsureLifespan(apiResponse) == false)
            {
                return apiResponse;
            }

            var client = new RestClient(_hostUrl);
            var restRequest = new RestRequest($"v1/api/interactions/activities/{conversation.ConversationId}", Method.GET) { Timeout = 20000 };

            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", $"Bearer {authorizer.Token.AccessToken}");

            restRequest.AddParameter("leid", lastEventId);
            restRequest.AddParameter("type", type.ToString());

            var restResponse = client.Execute(restRequest);
            apiResponse = ExtractResponse<IEnumerable<DirectActivityModel>>(restResponse);

            return apiResponse;
        }

        /// <summary>
        /// Sends to agent.
        /// </summary>
        /// <param name="authorizer">The authorizer.</param>
        /// <param name="conversation">The conversation.</param>
        /// <param name="activity">The activity.</param>
        /// <returns></returns>
        public ApiResponse<DirectResponseModel> SendToAgent(IHubsterAuthorizer authorizer, EstablishedConversationModel conversation, DirectActivityModel activity)
        {
            var apiResponse = Send(authorizer, conversation, activity, "customer");
            return apiResponse;
        }

        /// <summary>
        /// Sends to customer.
        /// </summary>
        /// <param name="authorizer">The authorizer.</param>
        /// <param name="conversation">The conversation.</param>
        /// <param name="activity">The activity.</param>
        /// <returns></returns>
        public ApiResponse<DirectResponseModel> SendToCustomer(IHubsterAuthorizer authorizer, EstablishedConversationModel conversation, DirectActivityModel activity)
        {
            var apiResponse = Send(authorizer, conversation, activity, "business");
            return apiResponse;
        }

        /// <summary>
        /// Sends to agent.
        /// </summary>
        /// <param name="authorizer">The authorizer.</param>
        /// <param name="conversation">The conversation.</param>
        /// <param name="activity">The activity.</param>
        /// <returns></returns>
        private ApiResponse<DirectResponseModel> Send(IHubsterAuthorizer authorizer, EstablishedConversationModel conversation, DirectActivityModel activity, string path)
        {
            var apiResponse = new ApiResponse<DirectResponseModel>();
            if (authorizer.EnsureLifespan(apiResponse) == false)
            {
                return apiResponse;
            }

            var client = new RestClient(_hostUrl);
            var restRequest = new RestRequest($"v1/inbound/{path}/direct/activity/{conversation.ConversationId}", Method.POST) { Timeout = 20000 };

            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", $"Bearer {authorizer.Token.AccessToken}");

            var body = JsonConvert.SerializeObject(activity);
            restRequest.AddParameter("application/json", body, ParameterType.RequestBody);

            var restResponse = client.Execute(restRequest);
            apiResponse = ExtractResponse<DirectResponseModel>(restResponse);

            return apiResponse;
        }
    }
}
