﻿// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Auth.Models;

namespace Hubster.Auth.Interfaces
{
    public interface IHubsterAuthClient : IHubsterAuth
    {
        IdentityResponse<IdentityToken> GetClientToken(string clientId, string secret);
        IdentityResponse<IdentityToken> GetUserToken(string username, string password);
        IdentityResponse<IdentityToken> RefreshToken(IdentityToken token);
    }
}