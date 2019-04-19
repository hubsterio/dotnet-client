using Newtonsoft.Json;

namespace Hubster.Direct.Models
{
    public class ExceptionResponse : ApiResponse
    {
        [JsonProperty("trackingId", NullValueHandling = NullValueHandling.Ignore)]
        public string TrackingId { get; set; }

        [JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public string Timestamp { get; set; }
    }
}
