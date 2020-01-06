// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Direct.Enums;
using Hubster.Direct.Models;
using Hubster.Direct.Models.Direct;
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