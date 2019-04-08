using Hubster.Direct.Enums;
using Hubster.Direct.Models;
using Hubster.Direct.Models.Direct;
using Hubster.Direct.RemoteAccess;
using System;
using System.Collections.Generic;

namespace Hubster.Direct
{
    /// <summary>
    /// 
    /// </summary>
    public class HubsterActivity
    {
        private readonly EngineActivityAccess _engineAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="HubsterActivity" /> class.
        /// </summary>
        /// <param name="hostUrl">The host URL.</param>
        internal HubsterActivity(string hostUrl)
        {            
            _engineAccess = new EngineActivityAccess(hostUrl);
        }

        /// <summary>
        /// Establishes the conversation.
        /// </summary>
        /// <param name="authorizer">The authorizer.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public ApiResponse<IEnumerable<DirectActivityModel>> Get(IHubsterAuthorizer authorizer, EstablishedConversationModel conversation, long lastEventId, IntegrationType type)
        {
            var apiResponse = _engineAccess.Get(authorizer, conversation, lastEventId, type);
            return apiResponse;
        }

        /// <summary>
        /// Gets the established conversation.
        /// </summary>
        /// <param name="authorizer">The authorizer.</param>
        /// <param name="conversation">The conversation.</param>
        /// <param name="activityModel">The activity model.</param>
        /// <returns></returns>
        public ApiResponse<DirectResponseModel> SendToAgent(IHubsterAuthorizer authorizer, EstablishedConversationModel conversation, DirectActivityModel activityModel)
        {
            var apiResponse = _engineAccess.SendToAgent(authorizer, conversation, activityModel);
            return apiResponse;
        }

        /// <summary>
        /// Sends to customer.
        /// </summary>
        /// <param name="authorizer">The authorizer.</param>
        /// <param name="conversation">The conversation.</param>
        /// <param name="activityModel">The activity model.</param>
        /// <returns></returns>
        public ApiResponse<DirectResponseModel> SendToCustomer(IHubsterAuthorizer authorizer, EstablishedConversationModel conversation, DirectActivityModel activityModel)
        {
            var apiResponse = _engineAccess.SendToCustomer(authorizer, conversation, activityModel);
            return apiResponse;
        }
    }
}
