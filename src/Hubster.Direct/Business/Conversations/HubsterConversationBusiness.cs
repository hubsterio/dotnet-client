﻿using Hubster.Direct.Interfaces;
using Hubster.Direct.Models;
using System;

namespace Hubster.Direct.Business.Conversations
{
    /// <summary>
    /// 
    /// </summary>
    public class HubsterConversationBusiness : HubsterConversationBase
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
            var apiResponse = _engineAccess.GetEstablishedConversation(authorizer, conversationId);
            return apiResponse;
        }
    }
}