// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Hubster.Direct.Models.Direct
{
    public class DirectContactModel
    {
        public DirectContactModel()
        {
            Addresses = new List<DirectrPropertyValue>();
            Phones = new List<DirectrPropertyValue>();
            Emails = new List<DirectrPropertyValue>();
            Custom = new List<DirectrPropertyValue>();
        }

        [JsonProperty("full_name", NullValueHandling = NullValueHandling.Ignore)]
        public string FullName { get; set; }

        [JsonProperty("first_name", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }

        [JsonProperty("last_name", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }

        [JsonProperty("user_name", NullValueHandling = NullValueHandling.Ignore)]
        public string UserName { get; set; }

        [JsonProperty("gender", NullValueHandling = NullValueHandling.Ignore)]
        public string Gender { get; set; }

        [JsonProperty("local", NullValueHandling = NullValueHandling.Ignore)]
        public string Locale { get; set; }

        [JsonProperty("time_zone", NullValueHandling = NullValueHandling.Ignore)]
        public string TimeZone { get; set; }

        [JsonProperty("image_url", NullValueHandling = NullValueHandling.Ignore)]
        public string ImageUrl { get; set; }

        [JsonProperty("addresses", NullValueHandling = NullValueHandling.Ignore)]
        public List<DirectrPropertyValue> Addresses { get; set; }

        [JsonProperty("phones", NullValueHandling = NullValueHandling.Ignore)]
        public List<DirectrPropertyValue> Phones { get; set; }

        [JsonProperty("emails", NullValueHandling = NullValueHandling.Ignore)]
        public List<DirectrPropertyValue> Emails { get; set; }

        [JsonProperty("custom", NullValueHandling = NullValueHandling.Ignore)]        
        public List<DirectrPropertyValue> Custom { get; set; }

        public bool IsEmpty()
        {
            var addresses = Addresses.Where(x => DirectrPropertyValue.IsEmpty(x) == false);
            var phones = Phones.Where(x => DirectrPropertyValue.IsEmpty(x) == false);
            var emails = Emails.Where(x => DirectrPropertyValue.IsEmpty(x) == false);
            var custom = Custom.Where(x => DirectrPropertyValue.IsEmpty(x) == false);

            var isEmpty = string.IsNullOrWhiteSpace(FullName)
                && string.IsNullOrWhiteSpace(FirstName)
                && string.IsNullOrWhiteSpace(LastName)
                && string.IsNullOrWhiteSpace(UserName)
                && string.IsNullOrWhiteSpace(Gender)
                && string.IsNullOrWhiteSpace(Locale)
                && string.IsNullOrWhiteSpace(TimeZone)
                && string.IsNullOrWhiteSpace(ImageUrl)
                && addresses.Count() == 0
                && phones.Count() == 0
                && emails.Count() == 0
                && custom.Count() == 0;

            return isEmpty;
        }

        public void RemoveEmpties()
        {
            Addresses = Addresses.Where(x => DirectrPropertyValue.IsEmpty(x) == false).ToList();
            Phones = Phones.Where(x => DirectrPropertyValue.IsEmpty(x) == false).ToList();
            Emails = Emails.Where(x => DirectrPropertyValue.IsEmpty(x) == false).ToList();
            Custom = Custom.Where(x => DirectrPropertyValue.IsEmpty(x) == false).ToList();
        }
    }
}
