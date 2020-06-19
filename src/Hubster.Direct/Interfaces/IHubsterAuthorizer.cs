// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Abstractions.Models;
using Hubster.Auth.Interfaces;
using Hubster.Auth.Models;

namespace Hubster.Direct.Interfaces
{
    public interface IHubsterAuthorizer
    {
        IHubsterAuth AuthClient { get; }
        IdentityToken Token { get; }

        bool EnsureLifespan(ApiResponse apiResponse);
    }
}