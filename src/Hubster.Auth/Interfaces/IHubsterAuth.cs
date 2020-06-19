// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Auth.Models;

namespace Hubster.Auth.Interfaces
{
    public interface IHubsterAuth
    {
        IdentityResponse<IdentityToken> EnsureLifespan(IdentityToken token = null);
    }
}