// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Auth;
using Hubster.Auth.Interfaces;
using Hubster.Auth.Models;
using Hubster.Abstractions.Models;

namespace Hubster.Direct.Interfaces
{
    public interface IHubsterAuthorizer
    {
        IHubsterAuthClient AuthClient { get; }
        IdentityToken Token { get; }

        bool EnsureLifespan(ApiResponse apiResponse);
    }
}