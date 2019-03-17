using Newtonsoft.Json;

namespace Hubster.Auth
{
    internal class IdentityToken
    {
        [JsonProperty("access_token", NullValueHandling = NullValueHandling.Ignore)]
        public string AccessToken { get; internal set; }

        [JsonProperty("refresh_token", NullValueHandling = NullValueHandling.Ignore)]
        public string RefreshToken { get; internal set; }

        [JsonProperty("token_type", NullValueHandling = NullValueHandling.Ignore)]
        public string TokenType { get; internal set; }

        [JsonProperty("expires_in", NullValueHandling = NullValueHandling.Ignore)]
        public int? Expires { get; internal set; }
    }
}
