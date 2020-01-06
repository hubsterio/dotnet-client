// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Newtonsoft.Json;
using System;

namespace Hubster.Direct.Models
{
    public class EstablishedConversationModel
    {
        [JsonProperty("tenant_id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? TenantId { get; set; }

        [JsonProperty("hub_id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? HubId { get; set; }

        [JsonProperty("integration_id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? IntegrationId { get; set; }

        [JsonProperty("conversation_id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? ConversationId { get; set; }

        [JsonProperty("token_id", NullValueHandling = NullValueHandling.Ignore)]
        public string TokenId { get; set; }

        [JsonProperty("properties", NullValueHandling = NullValueHandling.Ignore)]
        public ConversationPropertiesModel Properties { get; set; }

        [JsonProperty("opened", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? OpenedDateTime { get; set; }

        [JsonProperty("closed", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? ClosedDateTime { get; set; }
    }
}
