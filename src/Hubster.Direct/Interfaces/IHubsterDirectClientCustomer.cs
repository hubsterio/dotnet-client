// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Direct.Business.Activities;
using Hubster.Direct.Business.Conversations;
using Hubster.Direct.Events;

namespace Hubster.Direct.Interfaces
{
    public interface IHubsterDirectClientCustomer
    {
        IHubsterActivityCustomer Activity { get; }
        IHubsterConversationCustomer Conversation { get; }
        IHubsterEventsCustomer Events { get; }
    }
}