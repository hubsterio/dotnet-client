// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Auth.Interfaces;
using Hubster.Auth.Models;
using Hubster.Direct.RemoteAccess;
using System;
using System.Net;

namespace Hubster.Direct
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Hubster.Auth.IHubsterAuthClient" />
    public class HubsterAuthWebChat : IHubsterAuth
    {
        private readonly string _origin;
        private readonly Guid _integrationId;
        private readonly EngineIntegrationAccess _engineIntegration;
        

        /// <summary>
        /// Initializes a new instance of the <see cref="HubsterAuthWebChat" /> class.
        /// </summary>
        /// <param name="onAuthRequest">The on authentication request.</param>
        /// <param name="hostUrl">The host URL.</param>
        public HubsterAuthWebChat(Guid integrationId, string origin, string hostUrl = "https://engine.hubster.io")
        {
            _origin = origin;
            _integrationId = integrationId;
            _engineIntegration = new EngineIntegrationAccess(origin, hostUrl);
        }

        /// <summary>
        /// Refreshes the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        private IdentityResponse<IdentityToken> RefreshToken()
        {
            var tokeResponse = _engineIntegration.CreateWebChatTokenAsync(_integrationId);

            if(tokeResponse.StatusCode == HttpStatusCode.OK)
            {
                if (tokeResponse.Content.Expires != null)
                {
                    tokeResponse.Content.ExpireTime = DateTimeOffset.UtcNow.AddMinutes(tokeResponse.Content.Expires.Value);
                }
            }

            return new IdentityResponse<IdentityToken>
            {
                StatusCode = tokeResponse.StatusCode,
                StatusMessage = tokeResponse.StatusCode.ToString(),
                Token = tokeResponse.Content,
            };
        }

        /// <summary>
        /// Ensures the lifespan.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public IdentityResponse<IdentityToken> EnsureLifespan(IdentityToken token = null)
        {
            if(token?.HasExpired() == false)
            {
                return new IdentityResponse<IdentityToken>
                {
                    StatusCode = HttpStatusCode.OK,
                    StatusMessage = HttpStatusCode.OK.ToString(),
                    Token = token,                    
                };
            }

            var response = RefreshToken();
            return response;
        }
    }
}
