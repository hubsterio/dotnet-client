using Newtonsoft.Json;

namespace Hubster.Direct.Models.Direct
{
    public class DirectMediaModel
    {
        [JsonProperty("mime_type", NullValueHandling = NullValueHandling.Ignore)]
        public string MimeType { get; set; }

        [JsonProperty("storage_id", NullValueHandling = NullValueHandling.Ignore)]
        public string StorageId { get; set; }        
    }
}
