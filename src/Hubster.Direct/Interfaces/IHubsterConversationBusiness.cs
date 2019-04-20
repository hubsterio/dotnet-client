using Hubster.Direct.Models;
using System;
using System.Collections.Generic;

namespace Hubster.Direct.Interfaces
{
    public interface IHubsterConversationBusiness
    {
        ApiResponse<IEnumerable<EstablishedConversationModel>> GetAllEstablishedByHubId(IHubsterAuthorizer authorizer, Guid hubId);
        ApiResponse<EstablishedConversationModel> GetEstablished(IHubsterAuthorizer authorizer, Guid conversationId);
    }
}