// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

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
