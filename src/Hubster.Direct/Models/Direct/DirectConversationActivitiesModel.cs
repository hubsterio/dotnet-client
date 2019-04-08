using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hubster.Direct.Models.Direct
{

    public class DirectConversationActivitiesModel : DirectConversationModel
    {
        [JsonProperty("activities", NullValueHandling = NullValueHandling.Ignore)]
        public List<DirectActivityModel> Activities { get; set; }
    }
}
