using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace Hubster.Auth
{
    internal class IdentityAccess
    {
        private readonly string _hostUrl;

        public IdentityAccess(string hostUrl)
        {
            _hostUrl = hostUrl;
        }

        public TokenResponse<IdentityToken> GetUserToken(string username, string password)
        {
            var response = GetToken($"grant_type=password&username={username}&password={password}&client_id=hubster.portal");
            return response;
        }

        public TokenResponse<IdentityToken> GetClientToken(string clientId, string secret)
        {
            var response = GetToken($"grant_type=client_credentials&client_id={clientId}&client_secret={secret}");
            return response;
        }

        public TokenResponse<IdentityToken> GetUserRefreshToken(string refreshToken)
        {
            var response = GetToken($"grant_type=refresh_token&refresh_token={refreshToken}&client_id=hubster.portal");
            return response;
        }

        private TokenResponse<IdentityToken> GetToken(string body)
        {
            var client = new RestClient(_hostUrl);
            var request = new RestRequest("connect/token", Method.POST) { Timeout = 10000 };

            request.AddParameter("application/x-www-form-urlencoded", body, ParameterType.RequestBody);

            var serverResponse = client.Execute(request);
            var response = ExtractResponse(serverResponse);

            return response;
        }

        protected TokenResponse<IdentityToken> ExtractResponse(IRestResponse serverResponse)
        {
            var response = new TokenResponse<IdentityToken>();

            if (serverResponse.StatusCode == HttpStatusCode.OK)
            {
                response.ServerStatusCode = HttpStatusCode.OK;
                response.ServerStatusMessage = HttpStatusCode.OK.ToString();
                response.Token = JsonConvert.DeserializeObject<IdentityToken>(serverResponse.Content);
            }
            else
            {
                response.ServerStatusCode = serverResponse.StatusCode != 0 ? serverResponse.StatusCode : HttpStatusCode.BadGateway;
                response.ServerStatusMessage = response.ServerStatusCode.ToString();

                if(string.IsNullOrWhiteSpace(serverResponse.Content) == false)
                {
                    var error = JsonConvert.DeserializeObject<IdentityError>(serverResponse.Content);
                    response.ServerStatusMessage = $"error: '{error.Error}', description: '{error.ErrorDescription ?? "none"}'";
                }
            }

            return response;
        }
    }
}
