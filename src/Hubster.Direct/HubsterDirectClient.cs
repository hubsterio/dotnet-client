using Hubster.Auth;
using Hubster.Auth.Models;
using Hubster.Direct.Models;
using Hubster.Direct.RemoteAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hubster.Direct
{
    /// <summary>
    /// 
    /// </summary>
    public class HubsterDirectClient
    {
        private readonly EngineAccess _engineAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="HubsterDirectClient"/> class.
        /// </summary>
        /// <param name="authClient">The authentication client.</param>
        /// <param name="hostUrl">The host URL.</param>
        public HubsterDirectClient(HubsterAuthClient authClient, string hostUrl = "https://direct.hubster.io")
        {            
            _engineAccess = new EngineAccess(authClient, hostUrl);
        }

        /// <summary>
        /// Establishes the conversation.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public ApiResponse<EstablishedConversationModel> EstablishConversation(EstablishConversationRequestModel request)
        {
            var apiResponse = _engineAccess.EstablishConversation(request);
            return apiResponse;
        }

        /// <summary>
        /// Gets the established conversation.
        /// </summary>
        /// <param name="conversationId">The conversation identifier.</param>
        /// <returns></returns>
        public ApiResponse<EstablishedConversationModel> GetEstablishedConversation(Guid conversationId)
        {
            var apiResponse = _engineAccess.GetEstablishedConversation(conversationId);
            return apiResponse;
        }
    }
}
