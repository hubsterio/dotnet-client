// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Auth;
using Hubster.Auth.Interfaces;
using Hubster.Auth.Models;
using Hubster.Direct.Interfaces;
using Hubster.Abstractions.Models;
using System.Collections.Generic;
using System.Net;

namespace Hubster.Direct
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Hubster.Direct.Interfaces.IHubsterAuthorizer" />
    public class HubsterAuthorizer : IHubsterAuthorizer
    {
        public IHubsterAuthClient AuthClient { get; private set; }
        public IdentityToken Token { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HubsterAuthorizer"/> class.
        /// </summary>
        /// <param name="authClient">The authentication client.</param>
        /// <param name="token">The token.</param>
        public HubsterAuthorizer(IHubsterAuthClient authClient, IdentityToken token = null)
        {
            AuthClient = authClient;
            Token = token;
        }

        /// <summary>
        /// Ensures the lifespan.
        /// </summary>
        /// <param name="errorRsponse">The error response.</param>
        /// <returns></returns>
        public bool EnsureLifespan(ApiResponse apiResponse)
        {
            var identityResponse = AuthClient.EnsureLifespan(Token);

            if (identityResponse.StatusCode != HttpStatusCode.OK)
            {
                var errorMessage = identityResponse.StatusMessage;
                if (identityResponse.Token != null)
                {
                    var errorParts = new List<string> { identityResponse.Token.Error };
                    if (string.IsNullOrWhiteSpace(identityResponse.Token.ErrorDescription) == false)
                    {
                        errorParts.Add(identityResponse.Token.ErrorDescription);
                    }

                    errorMessage = string.Join(" - ", errorParts);
                }

                apiResponse.StatusCode = identityResponse.StatusCode;
                apiResponse.Errors = new List<ErrorCodeModel>
                {
                    new ErrorCodeModel { Code = (int)apiResponse.StatusCode, Description = errorMessage }
                };

                return false;
            }

            apiResponse.StatusCode = identityResponse.StatusCode;
            Token = identityResponse.Token;

            return true;
        }
    }
}
