// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Hubster.Abstractions.Models
{
    public class ApiResponse
    {
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public HttpStatusCode StatusCode { get; set; }

        [JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
        public List<ErrorCodeModel> Errors { get; set; }
    }

    public class ApiResponse<T> : ApiResponse where T: class
    {
        public T Content { get; internal set; }
    }
}
