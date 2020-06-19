// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using System.Net;

namespace Hubster.Auth.Models
{
    public class IdentityResponse<T> where T: class
    {
        public HttpStatusCode StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public T Token { get; set; }
    }
}
