using Hubster.Direct.Interfaces;
using Hubster.Direct.Models;
using Hubster.Direct.Models.Direct;
using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace Hubster.Direct.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class HubsterEventsCustomer : HubsterEventsBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="HubsterEventsCustomer" /> class.
        /// </summary>
        /// <param name="eventsUrl">The host URL.</param>
        internal HubsterEventsCustomer(string eventsUrl) : base(eventsUrl)
        {
        }

        /// <summary>
        /// Starts the specified authorizer.
        /// </summary>
        /// <param name="authorizer">The authorizer.</param>
        /// <param name="conversation">The conversation.</param>
        /// <param name="onActivity">The on activity.</param>
        /// <param name="onError">The on error.</param>
        /// <returns></returns>
        public ApiResponse<HubConnection> Start(IHubsterAuthorizer authorizer, EstablishedConversationModel conversation, Action<DirectActivityModel> onActivity, Action<ErrorCodeModel> onError)
        {
            var apiResponse = Start(authorizer, conversation.IntegrationId.Value, conversation.ConversationId.Value, onActivity, onError);

            return apiResponse;
        }
    }
}
