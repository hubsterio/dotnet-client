// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Abstractions.Models;
using Hubster.Auth.Models;
using RestSharp;
using System;

namespace Hubster.Direct.RemoteAccess
{
    /// <summary>
    /// 
    /// </summary>
    internal class EngineIntegrationAccess : EngineBaseAccess
    {
        private readonly string _origin;
        private readonly string _hostUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="EngineIntegrationAccess" /> class.
        /// </summary>
        /// <param name="hostUrl">The host URL.</param>
        public EngineIntegrationAccess(string origin, string hostUrl)
        {
            _origin = origin;
            _hostUrl = hostUrl;
        }

        /// <summary>
        /// Establishes the conversation.
        /// </summary>
        /// <param name="integrationId">The integration identifier.</param>
        /// <returns></returns>
        public ApiResponse<IdentityToken> CreateWebChatTokenAsync(Guid integrationId)
        {
            var client = new RestClient(_hostUrl);            
            var restRequest = new RestRequest($"/api/v1/integrations/web-chat/token/{integrationId}", Method.GET) { Timeout = 20000 };

            restRequest.AddHeader("Origin", _origin);            

            var restResponse = client.Execute(restRequest);
            var apiResponse = ExtractResponse<IdentityToken>(restResponse);

            return apiResponse;
        }
    }
}
