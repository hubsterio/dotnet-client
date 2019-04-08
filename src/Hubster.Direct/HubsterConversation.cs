using Hubster.Direct.Models;
using Hubster.Direct.RemoteAccess;
using System;

namespace Hubster.Direct
{
    /// <summary>
    /// 
    /// </summary>
    public class HubsterConversation
    {
        private readonly EngineConversationAccess _engineAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="HubsterConversation" /> class.
        /// </summary>
        /// <param name="hostUrl">The host URL.</param>
        internal HubsterConversation(string hostUrl)
        {            
            _engineAccess = new EngineConversationAccess(hostUrl);
        }

        /// <summary>
        /// Establishes the conversation.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public ApiResponse<EstablishedConversationModel> Establish(IHubsterAuthorizer authorizer, EstablishConversationRequestModel request)
        {
            var apiResponse = _engineAccess.EstablishConversation(authorizer, request);
            return apiResponse;
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
