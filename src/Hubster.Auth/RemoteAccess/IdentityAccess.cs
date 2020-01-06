// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Auth.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;

namespace Hubster.Auth.RemoteAccess
{
    /// <summary>
    /// 
    /// </summary>
    internal class IdentityAccess
    {
        private readonly string _hostUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityAccess"/> class.
        /// </summary>
        /// <param name="hostUrl">The host URL.</param>
        public IdentityAccess(string hostUrl)
        {
            _hostUrl = hostUrl;
        }

        /// <summary>
        /// Gets the user token.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public IdentityResponse<IdentityToken> GetUserToken(string username, string password)
        {
            var apiReponse = GetToken($"grant_type=password&username={username}&password={password}&client_id=hubster.portal");
            return apiReponse;
        }

        /// <summary>
        /// Gets the client token.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="secret">The secret.</param>
        /// <returns></returns>
        public IdentityResponse<IdentityToken> GetClientToken(string clientId, string secret)
        {
            var apiReponse = GetToken($"grant_type=client_credentials&client_id={clientId}&client_secret={secret}");
            return apiReponse;
        }

        /// <summary>
        /// Gets the user refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns></returns>
        public IdentityResponse<IdentityToken> GetUserRefreshToken(string refreshToken)
        {
            var apiReponse = GetToken($"grant_type=refresh_token&refresh_token={refreshToken}&client_id=hubster.portal");
            return apiReponse;
        }

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <returns></returns>
        private IdentityResponse<IdentityToken> GetToken(string body)
        {
            var client = new RestClient(_hostUrl);
            var request = new RestRequest("connect/token", Method.POST) { Timeout = 20000 };

            request.AddParameter("application/x-www-form-urlencoded", body, ParameterType.RequestBody);

            var restResponse = client.Execute(request);
            var apiReponse = ExtractResponse(restResponse);

            return apiReponse;
        }

        /// <summary>
        /// Extracts the response.
        /// </summary>
        /// <param name="restResponse">The server response.</param>
        /// <returns></returns>
        protected IdentityResponse<IdentityToken> ExtractResponse(IRestResponse restResponse)
        {
            var apiResponse = new IdentityResponse<IdentityToken>();

            if (restResponse.StatusCode == HttpStatusCode.OK
            || restResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                apiResponse.StatusCode = restResponse.StatusCode;
                apiResponse.StatusMessage = restResponse.StatusCode.ToString();
                apiResponse.Token = JsonConvert.DeserializeObject<IdentityToken>(restResponse.Content);

                if (apiResponse.Token.Expires != null)
                {
                    apiResponse.Token.ExpireTime = DateTimeOffset.UtcNow.AddSeconds(apiResponse.Token.Expires.Value);
                }
            }
            else
            {
                apiResponse.StatusCode = restResponse.StatusCode;

                if (restResponse.StatusCode == 0)
                {
                    switch(restResponse.ResponseStatus)
                    {
                        case ResponseStatus.TimedOut:
                            apiResponse.StatusCode = HttpStatusCode.RequestTimeout;
                            break;

                        default:
                            apiResponse.StatusCode = HttpStatusCode.BadGateway;
                            break;
                    }
                }

                apiResponse.StatusMessage = apiResponse.StatusCode.ToString();
            }

            return apiResponse;
        }
    }
}
