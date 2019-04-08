using Hubster.Direct.Enums;
using Newtonsoft.Json;
using System;

namespace Hubster.Direct.Models.Direct
{
    public class DirectResponseModel
    {
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public ResponseStatus Status { get; set; }

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

        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public ErrorCodeModel Error { get; set; }
    }
}
