// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Newtonsoft.Json;

namespace Hubster.Direct.Models.Direct
{
    public class DirectrPropertyValue
    {
        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
        public string Key { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        public DirectrPropertyValue()
        {
        }

        public DirectrPropertyValue(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public static bool IsEmpty(DirectrPropertyValue property)
        {
            return property == null
                || string.IsNullOrWhiteSpace(property.Key) == true
                || string.IsNullOrWhiteSpace(property.Value) == true;
        }
    }
}
