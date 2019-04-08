using Newtonsoft.Json;
using System;

namespace Hubster.Direct.Models.Direct
{
    public class DirectConversationModel
    {
        [JsonProperty("tenant_id")]
        public Guid TenantId { get; set; }

        [JsonProperty("hub_id")]
        public Guid HubId { get; set; }

        [JsonProperty("conversation_id")]
        public Guid ConversationId { get; set; }

        [JsonProperty("event_id")]
        public long EventId { get; set; }
    }
}
