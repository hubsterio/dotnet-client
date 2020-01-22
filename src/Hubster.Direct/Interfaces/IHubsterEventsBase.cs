// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Abstractions.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace Hubster.Direct.Interfaces
{
    public interface IHubsterEventsBase
    {
        ApiResponse<HubConnection> Start(Action<StartOptions> start);
        void Stop(HubConnection connection);
    }
}