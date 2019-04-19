﻿using Hubster.Direct.Enums;
using Hubster.Direct.Interfaces;
using Hubster.Direct.Models;
using Hubster.Direct.Models.Direct;
using System.Collections.Generic;

namespace Hubster.Direct.Business.Activities
{
    /// <summary>
    /// 
    /// </summary>
    public class HubsterActivityCustomer : HubsterActivityBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HubsterActivityCustomer"/> class.
        /// </summary>
        /// <param name="directUrl">The direct URL.</param>
        internal HubsterActivityCustomer(string directUrl) : base(directUrl)
        {            
        }

        /// <summary>
        /// Gets the specified authorizer.
        /// </summary>
        /// <param name="authorizer">The authorizer.</param>
        /// <param name="conversation">The conversation.</param>
        /// <param name="lastEventId">The last event identifier.</param>
        /// <returns></returns>
        public ApiResponse<IEnumerable<DirectActivityModel>> Get(IHubsterAuthorizer authorizer, EstablishedConversationModel conversation, long lastEventId)
        {
            var apiResponse = _engineAccess.Get(authorizer, conversation.ConversationId.Value, lastEventId, IntegrationType.Customer);
            return apiResponse;
        }

        /// <summary>
        /// Sends the specified authorizer.
        /// </summary>
        /// <param name="authorizer">The authorizer.</param>
        /// <param name="conversation">The conversation.</param>
        /// <param name="activity">The activity.</param>
        /// <returns></returns>
        public ApiResponse<DirectResponseModel> Send(IHubsterAuthorizer authorizer, EstablishedConversationModel conversation, DirectActivityModel activity)
        {
            var apiResponse = _engineAccess.Send(authorizer, conversation.ConversationId.Value, activity, "Customer");
            return apiResponse;
        }
    }
}