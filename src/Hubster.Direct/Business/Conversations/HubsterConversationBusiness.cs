// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Direct.Interfaces;
using Hubster.Abstractions.Models;
using System;
using System.Collections.Generic;

namespace Hubster.Direct.Business.Conversations
{
    /// <summary>
    /// 
    /// </summary>
    public class HubsterConversationBusiness : HubsterConversationBase, IHubsterConversationBusiness
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HubsterConversationBusiness" /> class.
        /// </summary>
        /// <param name="directUrl">The host URL.</param>
        internal HubsterConversationBusiness(string directUrl) : base(directUrl)
        {            
        }

        /// <summary>
        /// Gets the established conversation.
        /// </summary>
        /// <param name="conversationId">The conversation identifier.</param>
        /// <returns></returns>
        public ApiResponse<EstablishedConversationModel> GetEstablished(IHubsterAuthorizer authorizer, Guid conversationId)
        {
            var apiResponse = _engineAccess.GetEstablished(authorizer, conversationId);
            return apiResponse;
        }

        /// <summary>
        /// Gets all established by hub identifier.
        /// </summary>
        /// <param name="authorizer">The authorizer.</param>
        /// <param name="hubId">The hub identifier.</param>
        /// <returns></returns>
        public ApiResponse<IEnumerable<EstablishedConversationModel>> GetAllEstablishedByHubId(IHubsterAuthorizer authorizer, Guid hubId)
        {
            var apiResponse = _engineAccess.GetAllEstablishedByHubId(authorizer, hubId);
            return apiResponse;
        }
    }
}
