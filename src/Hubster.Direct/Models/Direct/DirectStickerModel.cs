﻿using Newtonsoft.Json;

namespace Hubster.Direct.Models.Direct
{
    public class DirectStickerModel
    {
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }        
    }
}