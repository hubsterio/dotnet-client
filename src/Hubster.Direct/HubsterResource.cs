using Hubster.Direct.Models;
using Hubster.Direct.RemoteAccess;
using System;

namespace Hubster.Direct
{
    /// <summary>
    /// 
    /// </summary>
    public class HubsterResource
    {
        private readonly EngineResourceAccess _engineAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="HubsterResource" /> class.
        /// </summary>
        /// <param name="hostUrl">The host URL.</param>
        internal HubsterResource(string hostUrl)
        {            
            _engineAccess = new EngineResourceAccess(hostUrl);
        }

        /// <summary>
        /// Establishes the conversation.
        /// </summary>
        /// <param name="authorizer">The authorizer.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public ApiResponse<EstablishedConversationModel> Get(IHubsterAuthorizer authorizer, EstablishConversationRequestModel request)
        {
            var apiResponse = _engineAccess.Get(authorizer, request);
            return apiResponse;
        }

        /// <summary>
        /// Gets the established conversation.
        /// </summary>
        /// <param name="authorizer">The authorizer.</param>
        /// <param name="conversationId">The conversation identifier.</param>
        /// <returns></returns>
        public ApiResponse<EstablishedConversationModel> Send(IHubsterAuthorizer authorizer, Guid conversationId)
        {
            var apiResponse = _engineAccess.Send(authorizer, conversationId);
            return apiResponse;
        }
    }
}
