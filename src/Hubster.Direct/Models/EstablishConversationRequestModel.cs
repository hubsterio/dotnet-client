// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hubster.Direct.Models
{
    public class EstablishConversationRequestModel
    {
        public EstablishConversationRequestModel()
        {
            Properties = new ConversationPropertiesModel
            {
                Profile = new Dictionary<string, string>(),
                Additional = new Dictionary<string, string>()
            };
        }

        [JsonProperty("integration_id")]
        public string IntegrationId { get; set; }

        [JsonProperty("binding")]
        public string Binding { get; set; }

        [JsonProperty("properties")]
        public ConversationPropertiesModel Properties { get; set; }
    }
}
