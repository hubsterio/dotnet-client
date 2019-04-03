using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Hubster.Direct.Models
{
    public class ExceptionResponse
    {
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public HttpStatusCode? Status { get; set; }

        [JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
        public List<ErrorCodeModel> Errors { get; set; }

        [JsonProperty("trackingId", NullValueHandling = NullValueHandling.Ignore)]
        public string TrackingId { get; set; }

        [JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public string Timestamp { get; set; }
    }
}
