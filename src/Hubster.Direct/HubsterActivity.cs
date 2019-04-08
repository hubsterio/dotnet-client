using Hubster.Auth;
using Hubster.Direct.Models;
using Hubster.Direct.RemoteAccess;
using System;

namespace Hubster.Direct
{
    /// <summary>
    /// 
    /// </summary>
    public class HubsterActivity
    {
        private readonly EngineActivityAccess _engineAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="HubsterActivity"/> class.
        /// </summary>
        /// <param name="authClient">The authentication client.</param>
        /// <param name="hostUrl">The host URL.</param>
        internal HubsterActivity(HubsterAuthClient authClient, string hostUrl)
        {            
            _engineAccess = new EngineActivityAccess(authClient, hostUrl);
        }

        /// <summary>
        /// Establishes the conversation.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public ApiResponse<EstablishedConversationModel> Get(EstablishConversationRequestModel request)
        {
            var apiResponse = _engineAccess.Get(request);
            return apiResponse;
        }

        /// <summary>
        /// Gets the established conversation.
        /// </summary>
        /// <param name="conversationId">The conversation identifier.</param>
        /// <returns></returns>
        public ApiResponse<EstablishedConversationModel> Send(Guid conversationId)
        {
            var apiResponse = _engineAccess.Send(conversationId);
            return apiResponse;
        }
    }
}
