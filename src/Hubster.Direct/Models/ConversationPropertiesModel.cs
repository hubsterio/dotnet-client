﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hubster.Direct.Models
{
    public class ConversationPropertiesModel
    {
        [JsonProperty("profile")]
        public Dictionary<string, string> Profile { get; set; }

        [JsonProperty("additional")]
        public Dictionary<string, string> Additional { get; set; }
    }
}