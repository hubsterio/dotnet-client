using Hubster.Auth;
using Hubster.Auth.Models;
using Hubster.Direct.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace Hubster.Direct.RemoteAccess
{
    /// <summary>
    /// 
    /// </summary>
    internal abstract class EngineBaseAccess
    {
        protected readonly string _hostUrl;
        protected readonly IHubsterAuthClient _authClient;
        protected IdentityToken _token;

        /// <summary>
        /// Initializes a new instance of the <see cref="EngineBaseAccess" /> class.
        /// </summary>
        /// <param name="authClient">The authentication client.</param>
        /// <param name="hostUrl">The host URL.</param>
        public EngineBaseAccess(IHubsterAuthClient authClient, string hostUrl)
        {
            _authClient = authClient;
            _hostUrl = hostUrl;
        }

        /// Extracts the response.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="restResponse">The rest response.</param>
        /// <returns></returns>
        protected ApiResponse<T> ExtractResponse<T>(IRestResponse restResponse) where T: class
        {
            var apiResponse = new ApiResponse<T>();

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                apiResponse.StatusCode = restResponse.StatusCode;
                apiResponse.Content = JsonConvert.DeserializeObject<T>(restResponse.Content);
            }
            else
            {
                apiResponse.StatusCode = restResponse.StatusCode != 0 
                    ? restResponse.StatusCode 
                    : HttpStatusCode.BadGateway;

                if (string.IsNullOrWhiteSpace(restResponse.Content) == false)
                {
                    var exceptionResponse = JsonConvert.DeserializeObject<ExceptionResponse>(restResponse.Content);
                    apiResponse.Errors = exceptionResponse.Errors;
                }
                else
                {
                    apiResponse.Errors = new List<ErrorCodeModel>
                    {
                        new ErrorCodeModel { Code = (int)apiResponse.StatusCode, Description = apiResponse.StatusCode.ToString() }
                    };
                }
            }

            return apiResponse;
        }

        /// <summary>
        /// Ensures the lifespan.
        /// </summary>
        /// <param name="apiResponse">The API response.</param>
        /// <returns></returns>
        protected bool EnsureLifespan(ApiResponse apiResponse)
        {
            var identityResponse = _authClient.EnsureLifespan(_token);

            if(identityResponse.StatusCode != HttpStatusCode.OK)
            {
                var errorMessage = identityResponse.StatusMessage;
                if (identityResponse.Token != null)
                {
                    var errorParts = new List<string> { identityResponse.Token.Error };
                    if(string.IsNullOrWhiteSpace(identityResponse.Token.ErrorDescription) == false)
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

            _token = identityResponse.Token;
            return true;
        }
    }
}
