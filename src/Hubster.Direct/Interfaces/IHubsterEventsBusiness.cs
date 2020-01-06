// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Direct.Models;
using Hubster.Direct.Models.Direct;
using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace Hubster.Direct.Interfaces
{
    public interface IHubsterEventsBusiness
    {
        ApiResponse<HubConnection> Start(IHubsterAuthorizer authorizer, Guid integrationId, Action<DirectActivityModel> onActivity, Action<ErrorCodeModel> onError);
        void Stop(HubConnection connection);
    }
}