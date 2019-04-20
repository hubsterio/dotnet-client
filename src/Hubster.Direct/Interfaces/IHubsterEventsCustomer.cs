using Hubster.Direct.Models;
using Hubster.Direct.Models.Direct;
using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace Hubster.Direct.Interfaces
{
    public interface IHubsterEventsCustomer
    {
        ApiResponse<HubConnection> Start(IHubsterAuthorizer authorizer, EstablishedConversationModel conversation, Action<DirectActivityModel> onActivity, Action<ErrorCodeModel> onError);
        void Stop(HubConnection connection);
    }
}