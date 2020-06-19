// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Abstractions.Enums;
using Hubster.Direct.Interfaces;
using Hubster.Abstractions.Models;
using Hubster.Abstractions.Models.Direct;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Hubster.Direct.RemoteAccess
{
    /// <summary>
    /// 
    /// </summary>
    internal class EngineActivityAccess : EngineBaseAccess
    {
        private readonly string _origin;
        private readonly string _hostUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="EngineActivityAccess" /> class.
        /// </summary>
        /// <param name="hostUrl">The host URL.</param>
        public EngineActivityAccess(string origin, string hostUrl) 
        {
            _origin = origin;
            _hostUrl = hostUrl;
        }

        /// <summary>
        /// Establishes the conversation.
        /// </summary>
        /// <param name="authorizer">The authorizer.</param>
        /// <param name="conversationId">The conversation identifier.</param>
        /// <param name="lastEventId">The last event identifier.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public ApiResponse<IEnumerable<DirectActivityModel>> Get(IHubsterAuthorizer authorizer, Guid conversationId, long lastEventId, IntegrationType type)
        {
            var apiResponse = new ApiResponse<IEnumerable<DirectActivityModel>>();
            if(authorizer.EnsureLifespan(apiResponse) == false)
            {
                return apiResponse;
            }

            var client = new RestClient(_hostUrl);
            var restRequest = new RestRequest($"/api/v1/interactions/activities/{conversationId}", Method.GET) { Timeout = 20000 };

            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", $"{authorizer.Token.TokenType} {authorizer.Token.AccessToken}");
            restRequest.AddHeader("Origin", _origin);

            restRequest.AddParameter("leid", lastEventId);
            restRequest.AddParameter("type", type.ToString());

            var restResponse = client.Execute(restRequest);
            apiResponse = ExtractResponse<IEnumerable<DirectActivityModel>>(restResponse);

            return apiResponse;
        }

        /// <summary>
        /// Sends the specified authorizer.
        /// </summary>
        /// <param name="authorizer">The authorizer.</param>
        /// <param name="conversationId">The conversation identifier.</param>
        /// <param name="activity">The activity.</param>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public ApiResponse<DirectResponseModel> Send(IHubsterAuthorizer authorizer, Guid conversationId, DirectActivityModel activity, string path)
        {
            var apiResponse = new ApiResponse<DirectResponseModel>();
            if (authorizer.EnsureLifespan(apiResponse) == false)
            {
                return apiResponse;
            }

            var url = $"/inbound/{path}/v1/direct/activity/{conversationId}";
            if(authorizer.Token.TokenType == "WebBearer")
            {
                url = $"/inbound/customer/v1/web-chat/{conversationId}";
            }

            var client = new RestClient(_hostUrl);
            var restRequest = new RestRequest(url, Method.POST) { Timeout = 20000 };

            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", $"{authorizer.Token.TokenType} {authorizer.Token.AccessToken}");
            restRequest.AddHeader("Origin", _origin);

            var body = JsonConvert.SerializeObject(activity);
            restRequest.AddParameter("application/json", body, ParameterType.RequestBody);

            var restResponse = client.Execute(restRequest);
            apiResponse = ExtractResponse<DirectResponseModel>(restResponse);

            return apiResponse;
        }
    }
}
