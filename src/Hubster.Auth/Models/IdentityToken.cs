// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Newtonsoft.Json;
using System;

namespace Hubster.Auth.Models
{
    public class IdentityToken
    {
        [JsonProperty("access_token", NullValueHandling = NullValueHandling.Ignore)]
        public string AccessToken { get; internal set; }

        [JsonProperty("refresh_token", NullValueHandling = NullValueHandling.Ignore)]
        public string RefreshToken { get; internal set; }

        [JsonProperty("token_type", NullValueHandling = NullValueHandling.Ignore)]
        public string TokenType { get; internal set; }

        [JsonProperty("expires_in", NullValueHandling = NullValueHandling.Ignore)]
        public int? Expires { get; internal set; }

        [JsonProperty("expire_time", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset ExpireTime { get; set; }

        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; internal set; }

        [JsonProperty("error_description", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorDescription { get; internal set; }

        public bool HasExpired()
        {
            // shorten the expire date by 1 minutes
            return DateTimeOffset.UtcNow > ExpireTime.AddMinutes(-2);
        }
    }
}
