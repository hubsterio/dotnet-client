// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Newtonsoft.Json;
using System;

namespace Hubster.Direct.Models.Direct
{
    public class DirectInteractionModel
    {
        [JsonProperty("tenant_id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid TenantId { get; set; }

        [JsonProperty("hub_id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid HubId { get; set; }

        [JsonProperty("conversation_id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid ConversationId { get; set; }

        [JsonProperty("activity", NullValueHandling = NullValueHandling.Ignore)]
        public DirectActivityModel Activity { get; set; }
    }
}
