﻿// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Abstractions.Enums;
using Hubster.Direct.Interfaces;
using Hubster.Abstractions.Models;
using Hubster.Abstractions.Models.Direct;
using System;
using System.Collections.Generic;

namespace Hubster.Direct.Business.Activities
{
    /// <summary>
    /// 
    /// </summary>
    public class HubsterActivityBusiness : HubsterActivityBase, IHubsterActivityBusiness
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HubsterActivityBusiness" /> class.
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="directUrl">The direct URL.</param>
        internal HubsterActivityBusiness(string origin, string directUrl) : base(origin, directUrl)
        {            
        }

        /// <summary>
        /// Gets the specified authorizer.
        /// </summary>
        /// <param name="authorizer">The authorizer.</param>
        /// <param name="conversationId">The conversation identifier.</param>
        /// <param name="lastEventId">The last event identifier.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public ApiResponse<IEnumerable<DirectActivityModel>> Get(IHubsterAuthorizer authorizer, Guid conversationId, long lastEventId, IntegrationType type)
        {
            var apiResponse = _engineAccess.Get(authorizer, conversationId, lastEventId, type);
            return apiResponse;
        }

        /// <summary>
        /// Sends the specified authorizer.
        /// </summary>
        /// <param name="authorizer">The authorizer.</param>
        /// <param name="conversationId">The conversation identifier.</param>
        /// <param name="activity">The activity model.</param>
        /// <returns></returns>
        public ApiResponse<DirectResponseModel> Send(IHubsterAuthorizer authorizer, Guid conversationId, DirectActivityModel activity)
        {
            var apiResponse = _engineAccess.Send(authorizer, conversationId, activity, "business");
            return apiResponse;
        }
    }
}
