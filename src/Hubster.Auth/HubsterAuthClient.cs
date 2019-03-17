using System;
using System.Net;

namespace Hubster.Auth
{
    public class HubsterAuthClient
    {
        private readonly IdentityAccess _identityAccess;

        public string HostUrl { get; private set; }

        public HubsterAuthClient(string hostUrl = "identity.hubster.io")
        {
            HostUrl = hostUrl;
            _identityAccess = new IdentityAccess(HostUrl);
        }

        public TokenResponse<UserToken> GetUserToken(string username, string password)
        {
            var response = new TokenResponse<UserToken>();

            if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                response.ServerStatusCode = HttpStatusCode.BadRequest;
                response.ServerStatusMessage = "Username and password are required.";

                return response;
            }
            
            var identityResponse = _identityAccess.GetUserToken(username, password);
            response.ServerStatusCode = identityResponse.ServerStatusCode;
            response.ServerStatusMessage = identityResponse.ServerStatusMessage;

            if (identityResponse.ServerStatusCode == HttpStatusCode.OK)
            {
                response.Token = new UserToken
                {
                    Kind = TokenKind.User,
                    AccessToken = identityResponse.Token.AccessToken,
                    RefreshToken = identityResponse.Token.RefreshToken,
                    TokenType = identityResponse.Token.TokenType,
                    Expires = DateTimeOffset.UtcNow.AddSeconds(identityResponse.Token.Expires.Value)
                };
            }

            return response;
        }

        public TokenResponse<UserToken> RefreshToken(string refreshToken)
        {
            var response = new TokenResponse<UserToken>();

            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                response.ServerStatusCode = HttpStatusCode.BadRequest;
                response.ServerStatusMessage = "RefreshToken is required.";

                return response;
            }
            
            var identityResponse = _identityAccess.GetUserRefreshToken(refreshToken);
            response.ServerStatusCode = identityResponse.ServerStatusCode;
            response.ServerStatusMessage = identityResponse.ServerStatusMessage;

            if (identityResponse.ServerStatusCode == HttpStatusCode.OK)
            {
                response.Token = new UserToken
                {
                    Kind = TokenKind.User,
                    AccessToken = identityResponse.Token.AccessToken,
                    RefreshToken = identityResponse.Token.RefreshToken,
                    TokenType = identityResponse.Token.TokenType,
                    Expires = DateTimeOffset.UtcNow.AddSeconds(identityResponse.Token.Expires.Value)
                };
            }

            return response;
        }

        public TokenResponse<UserToken> RefreshToken(UserToken token)
        {
            return RefreshToken(token?.RefreshToken);
        }

        public TokenResponse<Token> GetClientToken(string clientId, string secret)
        {
            var response = new TokenResponse<Token>();

            if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(secret))
            {
                response.ServerStatusCode = HttpStatusCode.BadRequest;
                response.ServerStatusMessage = "clientId and secret are required.";

                return response;
            }
            
            var identityResponse = _identityAccess.GetClientToken(clientId, secret);
            response.ServerStatusCode = identityResponse.ServerStatusCode;
            response.ServerStatusMessage = identityResponse.ServerStatusMessage;

            if (identityResponse.ServerStatusCode == HttpStatusCode.OK)
            {
                response.Token = new Token
                {
                    Kind = TokenKind.Client,
                    AccessToken = identityResponse.Token.AccessToken,                    
                    TokenType = identityResponse.Token.TokenType,
                    Expires = DateTimeOffset.UtcNow.AddSeconds(identityResponse.Token.Expires.Value)
                };
            }

            return response;
        }

        public TokenResponse<UserToken> EnsureLifespand(UserToken token)
        {
            if(token.HasExpired() == false)
            {
                return new TokenResponse<UserToken>
                {
                    ServerStatusCode = HttpStatusCode.OK,
                    ServerStatusMessage = HttpStatusCode.OK.ToString(),
                    Token = token,                    
                };
            }
            
            var identityResponse = _identityAccess.GetUserRefreshToken(token.RefreshToken);
            var response = new TokenResponse<UserToken>
            {
                ServerStatusCode = identityResponse.ServerStatusCode,
                ServerStatusMessage = identityResponse.ServerStatusMessage
            };

            if (identityResponse.ServerStatusCode == HttpStatusCode.OK)
            {
                response.Token = new UserToken
                {
                    Kind = TokenKind.User,
                    AccessToken = identityResponse.Token.AccessToken,
                    RefreshToken = identityResponse.Token.RefreshToken,
                    TokenType = identityResponse.Token.TokenType,
                    Expires = DateTimeOffset.UtcNow.AddSeconds(identityResponse.Token.Expires.Value)
                };
            }

            return response;
        }

        public TokenResponse<Token> EnsureLifespand(Token token)
        {
            if (token.HasExpired() == false)
            {
                return new TokenResponse<Token>
                {
                    ServerStatusCode = HttpStatusCode.OK,
                    ServerStatusMessage = HttpStatusCode.OK.ToString(),
                    Token = token,
                };
            }

            return new TokenResponse<Token>
            {
                ServerStatusCode = HttpStatusCode.Unauthorized,
                ServerStatusMessage = HttpStatusCode.Unauthorized.ToString(),                
            };
        }
    }
}
