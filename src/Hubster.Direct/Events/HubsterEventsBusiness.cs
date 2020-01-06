// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

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
    /// <seealso cref="Hubster.Direct.HubsterEventsBase" />
    public class HubsterEventsBusiness : HubsterEventsBase, IHubsterEventsBusiness
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="HubsterEventsBusiness" /> class.
        /// </summary>
        /// <param name="eventsUrl">The host URL.</param>
        internal HubsterEventsBusiness(string eventsUrl) : base(eventsUrl)
        {
        }

        /// <summary>
        /// Starts the specified authorizer.
        /// </summary>
        /// <param name="authorizer">The authorizer.</param>
        /// <param name="integrationId">The integration identifier.</param>
        /// <param name="onActivity">The on activity.</param>
        /// <param name="onError">The on error.</param>
        /// <returns></returns>
        public ApiResponse<HubConnection> Start(IHubsterAuthorizer authorizer, Guid integrationId, Action<DirectActivityModel> onActivity, Action<ErrorCodeModel> onError)
        {
            var apiResponse = Start(authorizer, integrationId, null, onActivity, onError);

            return apiResponse;
        }
    }
}
