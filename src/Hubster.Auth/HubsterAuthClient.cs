using Hubster.Auth.Models;
using Hubster.Auth.RemoteAccess;
using System;
using System.Net;

namespace Hubster.Auth
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Hubster.Auth.IHubsterAuthClient" />
    public class HubsterAuthClient : IHubsterAuthClient
    {
        private readonly IdentityAccess _identityAccess;
        private readonly Func<HubsterAuthClient, Models.IdentityResponse<IdentityToken>> _onAuthorizationRequest;

        public string HostUrl { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HubsterAuthClient" /> class.
        /// </summary>
        /// <param name="onAuthorizationRequest">The on authorization request.</param>
        /// <param name="hostUrl">The host URL.</param>
        public HubsterAuthClient(Func<HubsterAuthClient, IdentityResponse<IdentityToken>> onAuthorizationRequest, string hostUrl = "https://identity.hubster.io")
        {
            HostUrl = hostUrl;
            _identityAccess = new IdentityAccess(HostUrl);
            _onAuthorizationRequest = onAuthorizationRequest;            
        }

        /// <summary>
        /// Gets the user token.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public IdentityResponse<IdentityToken> GetUserToken(string username, string password)
        {
            if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return new IdentityResponse<IdentityToken>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    StatusMessage = "username and password are required.",
                };
            }
            
            var response = _identityAccess.GetUserToken(username, password);
            return response;
        }

        /// <summary>
        /// Refreshes the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public IdentityResponse<IdentityToken> RefreshToken(IdentityToken token)
        {
            if(DateTimeOffset.UtcNow >= token?.ExpireTime 
            || string.IsNullOrWhiteSpace(token?.RefreshToken) == true)
            {
                var authResponse = _onAuthorizationRequest?.Invoke(this);

                return authResponse ?? new IdentityResponse<IdentityToken>
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    StatusMessage = HttpStatusCode.Unauthorized.ToString(),
                };
            }

            var response = _identityAccess.GetUserRefreshToken(token?.RefreshToken);
            return response;
        }

        /// <summary>
        /// Gets the client token.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="secret">The secret.</param>
        /// <returns></returns>
        public IdentityResponse<IdentityToken> GetClientToken(string clientId, string secret)
        {
            if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(secret))
            {
                return new IdentityResponse<IdentityToken>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    StatusMessage = "clientId and secret are required.",
                };
            }
            
            var response = _identityAccess.GetClientToken(clientId, secret);
            return response;
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

            var response = RefreshToken(token);
            return response;
        }
    }
}
