// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Newtonsoft.Json;

namespace Hubster.Direct.Models.Direct
{
    public class DirectMediaModel
    {
        [JsonProperty("mime_type", NullValueHandling = NullValueHandling.Ignore)]
        public string MimeType { get; set; }

        [JsonProperty("storage_path", NullValueHandling = NullValueHandling.Ignore)]
        public string StoragePath { get; set; }        
    }
}
