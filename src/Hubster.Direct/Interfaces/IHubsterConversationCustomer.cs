﻿// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Abstractions.Models;

namespace Hubster.Direct.Interfaces
{
    public interface IHubsterConversationCustomer
    {
        ApiResponse<EstablishedConversationModel> Establish(IHubsterAuthorizer authorizer, EstablishConversationRequestModel request);
    }
}