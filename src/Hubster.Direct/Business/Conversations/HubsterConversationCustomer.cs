// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Direct.Interfaces;
using Hubster.Abstractions.Models;

namespace Hubster.Direct.Business.Conversations
{
    /// <summary>
    /// 
    /// </summary>
    public class HubsterConversationCustomer : HubsterConversationBase, IHubsterConversationCustomer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HubsterConversationCustomer" /> class.
        /// </summary>
        /// <param name="origin">The origin.</param>
        /// <param name="directUrl">The host URL.</param>
        internal HubsterConversationCustomer(string origin, string directUrl) : base(origin, directUrl)
        {            
        }

        /// <summary>
        /// Establishes the conversation.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public ApiResponse<EstablishedConversationModel> Establish(IHubsterAuthorizer authorizer, EstablishConversationRequestModel request)
        {
            var apiResponse = _engineAccess.Establish(authorizer, request);
            return apiResponse;
        }
    }
}
