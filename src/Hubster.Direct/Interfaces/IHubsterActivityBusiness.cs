// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Abstractions.Enums;
using Hubster.Abstractions.Models;
using Hubster.Abstractions.Models.Direct;
using System;
using System.Collections.Generic;

namespace Hubster.Direct.Interfaces
{
    public interface IHubsterActivityBusiness
    {
        ApiResponse<IEnumerable<DirectActivityModel>> Get(IHubsterAuthorizer authorizer, Guid conversationId, long lastEventId, IntegrationType type);
        ApiResponse<DirectResponseModel> Send(IHubsterAuthorizer authorizer, Guid conversationId, DirectActivityModel activity);
    }
}