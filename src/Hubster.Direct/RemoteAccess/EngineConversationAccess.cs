// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Direct.Interfaces;
using Hubster.Abstractions.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Hubster.Direct.RemoteAccess
{
    /// <summary>
    /// 
    /// </summary>
    internal class EngineConversationAccess : EngineBaseAccess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EngineConversationAccess" /> class.
        /// </summary>
        /// <param name="hostUrl">The host URL.</param>
        public EngineConversationAccess(string hostUrl) : base(hostUrl)
        {
        }

        /// <summary>
        /// Establishes the conversation.
        /// </summary>
        /// <param name="authorizer">The authorizer.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public ApiResponse<EstablishedConversationModel> Establish(IHubsterAuthorizer authorizer, EstablishConversationRequestModel request)
        {
            var apiResponse = new ApiResponse<EstablishedConversationModel>();
            if(authorizer.EnsureLifespan(apiResponse) == false)
            {
                return apiResponse;
            }

            var client = new RestClient(_hostUrl);
            var restRequest = new RestRequest("/api/v1/conversations/establish", Method.POST) { Timeout = 20000 };

            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", $"Bearer {authorizer.Token.AccessToken}");

            var body = JsonConvert.SerializeObject(request);
            restRequest.AddParameter("application/json", body, ParameterType.RequestBody);

            var restResponse = client.Execute(restRequest);
            apiResponse = ExtractResponse<EstablishedConversationModel>(restResponse);

            return apiResponse;
        }

        /// <summary>
        /// Gets the established conversation.
        /// </summary>
        /// <param name="authorizer">The authorizer.</param>
        /// <param name="conversationId">The conversation identifier.</param>
        /// <returns></returns>
        public ApiResponse<EstablishedConversationModel> GetEstablished(IHubsterAuthorizer authorizer, Guid conversationId)
        {
            var apiResponse = new ApiResponse<EstablishedConversationModel>();
            if (authorizer.EnsureLifespan(apiResponse) == false)
            {
                return apiResponse;
            }

            var client = new RestClient(_hostUrl);
            var restRequest = new RestRequest($"/api/v1/conversations/{conversationId}/established", Method.GET) { Timeout = 20000 };

            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", $"Bearer {authorizer.Token.AccessToken}");

            var restResponse = client.Execute(restRequest);
            apiResponse = ExtractResponse<EstablishedConversationModel>(restResponse);

            return apiResponse;
        }

        /// <summary>
        /// Gets all established by hub identifier.
        /// </summary>
        /// <param name="authorizer">The authorizer.</param>
        /// <param name="hubId">The hub identifier.</param>
        /// <returns></returns>
        public ApiResponse<IEnumerable<EstablishedConversationModel>> GetAllEstablishedByHubId(IHubsterAuthorizer authorizer, Guid hubId)
        {
            var apiResponse = new ApiResponse<IEnumerable<EstablishedConversationModel>>();
            if (authorizer.EnsureLifespan(apiResponse) == false)
            {
                return apiResponse;
            }

            var client = new RestClient(_hostUrl);
            var restRequest = new RestRequest($"/api/v1/conversations/hubs/{hubId}/established", Method.GET) { Timeout = 20000 };

            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", $"Bearer {authorizer.Token.AccessToken}");

            var restResponse = client.Execute(restRequest);
            apiResponse = ExtractResponse<IEnumerable<EstablishedConversationModel>>(restResponse);

            return apiResponse;
        }
    }
}
