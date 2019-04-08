using Hubster.Auth;
using Hubster.Direct.Models;
using Newtonsoft.Json;
using RestSharp;
using System;

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
        /// <param name="authClient">The authentication client.</param>
        /// <param name="hostUrl">The host URL.</param>
        public EngineActivityAccess(IHubsterAuthClient authClient, string hostUrl) : base(authClient, hostUrl)
        {
        }

        /// <summary>
        /// Establishes the conversation.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public ApiResponse<EstablishedConversationModel> Get(EstablishConversationRequestModel request)
        {
            var apiResponse = new ApiResponse<EstablishedConversationModel>();
            if(EnsureLifespan(apiResponse) == false)
            {
                return apiResponse;
            }

            var client = new RestClient(_hostUrl);
            var restRequest = new RestRequest("v1/api/conversations/establish", Method.POST) { Timeout = 10000 };

            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", $"Bearer {_token.AccessToken}");

            var body = JsonConvert.SerializeObject(request);
            restRequest.AddParameter("application/json", body, ParameterType.RequestBody);

            var restResponse = client.Execute(restRequest);
            apiResponse = ExtractResponse<EstablishedConversationModel>(restResponse);

            return apiResponse;
        }

        /// <summary>
        /// Gets the established conversation.
        /// </summary>
        /// <param name="conversationId">The conversation identifier.</param>
        /// <returns></returns>
        public ApiResponse<EstablishedConversationModel> Send(Guid conversationId)
        {
            var apiResponse = new ApiResponse<EstablishedConversationModel>();
            if (EnsureLifespan(apiResponse) == false)
            {
                return apiResponse;
            }

            var client = new RestClient(_hostUrl);
            var restRequest = new RestRequest($"v1/api/conversations/{conversationId}/established", Method.GET) { Timeout = 10000 };

            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", $"Bearer {_token.AccessToken}");

            var restResponse = client.Execute(restRequest);
            apiResponse = ExtractResponse<EstablishedConversationModel>(restResponse);

            return apiResponse;
        }
    }
}
