// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

namespace Hubster.Direct.Interfaces
{
    public interface IHubsterDirectClientBusiness
    {
        IHubsterActivityBusiness Activity { get; }
        IHubsterConversationBusiness Conversation { get; }
        IHubsterEventsBusiness Events { get; }
    }
}