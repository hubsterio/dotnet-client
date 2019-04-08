using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Hubster.Direct.Models.Direct
{
    public class DirectMessageModel
    {
        [JsonProperty("interaction_id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? InteractionId { get; set; }

        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        [JsonProperty("quick_actions", NullValueHandling = NullValueHandling.Ignore)]
        public List<DirectQuickReplyModel> QuickActions { get; set; }

        [JsonProperty("quick_reply", NullValueHandling = NullValueHandling.Ignore)]
        public DirectQuickReplyModel QuickReply { get; set; }

        [JsonProperty("attachments", NullValueHandling = NullValueHandling.Ignore)]
        public List<DirectAttachmentModel> Attachments { get; set; }
    }
}
