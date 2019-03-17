using Newtonsoft.Json;

namespace Hubster.Auth
{
    internal class IdentityError
    {
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; internal set; }

        [JsonProperty("error_description", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorDescription { get; internal set; }
    }
}
