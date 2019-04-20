using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace Hubster.Direct.Models.Direct
{
    public class DirectResponseModel
    {
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public HttpStatusCode Status { get; set; }

        [JsonProperty("event_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? EventId { get; set; }

        [JsonProperty("hub_id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? HubId { get; set; }
        
        [JsonProperty("conversation_id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? ConversationId { get; set; }

        [JsonProperty("integration_id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? IntegrationId { get; set; }

        [JsonProperty("interaction_id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? InteractionId { get; set; }

        [JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
        public List<ErrorCodeModel> Errors { get; set; }
    }
}
