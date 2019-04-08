using Newtonsoft.Json;

namespace Hubster.Direct.Models.Direct
{
    public class DirectAttachmentModel
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("comment", NullValueHandling = NullValueHandling.Ignore)]
        public string Comment { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("media", NullValueHandling = NullValueHandling.Ignore)]
        public DirectMediaModel Media { get; set; }

        [JsonProperty("sticker", NullValueHandling = NullValueHandling.Ignore)]
        public DirectStickerModel Sticker { get; set; }

        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public DirectLocationModel Location { get; set; }

        [JsonProperty("contact", NullValueHandling = NullValueHandling.Ignore)]
        public DirectContactModel Contact { get; set; }
    }
}
