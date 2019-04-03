using Hubster.Auth;
using Hubster.Auth.Models;
using Hubster.Direct.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace Hubster.Direct.RemoteAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class EngineAccess
    {
        private readonly string _hostUrl;
        private readonly IHubsterAuthClient _authClient;
        private IdentityToken _token;

        /// <summary>
        /// Initializes a new instance of the <see cref="EngineAccess" /> class.
        /// </summary>
        /// <param name="authClient">The authentication client.</param>
        /// <param name="hostUrl">The host URL.</param>
        public EngineAccess(IHubsterAuthClient authClient, string hostUrl)
        {
            _authClient = authClient;
            _hostUrl = hostUrl;
        }

        /// <summary>
        /// Establishes the conversation.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public ApiResponse<EstablishedConversationModel> EstablishConversation(EstablishConversationRequestModel request)
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
        public ApiResponse<EstablishedConversationModel> GetEstablishedConversation(Guid conversationId)
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

        /// <summary>
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
        private bool EnsureLifespan(ApiResponse apiResponse)
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
