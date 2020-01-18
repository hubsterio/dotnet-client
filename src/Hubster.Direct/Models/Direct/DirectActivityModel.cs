// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Direct.Enums;
using Newtonsoft.Json;

namespace Hubster.Direct.Models.Direct
{
    public class DirectActivityModel 
    {
        [JsonProperty("event_trigger", NullValueHandling = NullValueHandling.Ignore)]
        public string EventTrigger { get; set; }

        [JsonProperty("event_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? EventId { get; set; }

        [JsonProperty("flow_process", NullValueHandling = NullValueHandling.Ignore)]
        public ActivityFlowProcessType? FlowProcess { get; set; }

        [JsonProperty("sender", NullValueHandling = NullValueHandling.Ignore)]
        public DirectSourceModel Sender { get; set; }

        [JsonProperty("recipient", NullValueHandling = NullValueHandling.Ignore)]
        public DirectSourceModel Recipient { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
        
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public DirectMessageModel Message { get; set; }

        [JsonProperty("sender_action", NullValueHandling = NullValueHandling.Ignore)]
        public DirectSenderActionModel SenderAction { get; set; }
    }
}