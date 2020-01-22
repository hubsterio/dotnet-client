// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Newtonsoft.Json;

namespace Hubster.Abstractions.Models
{
    public class ExceptionResponse : ApiResponse
    {
        [JsonProperty("trackingId", NullValueHandling = NullValueHandling.Ignore)]
        public string TrackingId { get; set; }

        [JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public string Timestamp { get; set; }
    }
}
