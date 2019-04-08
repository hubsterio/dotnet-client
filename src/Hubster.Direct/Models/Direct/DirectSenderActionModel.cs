using Newtonsoft.Json;

namespace Hubster.Direct.Models.Direct
{
    public class DirectSenderActionModel
    {
        [JsonProperty("action", NullValueHandling = NullValueHandling.Ignore)]
        public string Action { get; set; }
    }
}
