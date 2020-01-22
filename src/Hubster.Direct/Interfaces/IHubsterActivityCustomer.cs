// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Abstractions.Models;
using Hubster.Abstractions.Models.Direct;
using System.Collections.Generic;

namespace Hubster.Direct.Interfaces
{
    public interface IHubsterActivityCustomer
    {
        ApiResponse<IEnumerable<DirectActivityModel>> Get(IHubsterAuthorizer authorizer, EstablishedConversationModel conversation, long lastEventId);
        ApiResponse<DirectResponseModel> Send(IHubsterAuthorizer authorizer, EstablishedConversationModel conversation, DirectActivityModel activity);
    }
}