using Hubster.Direct.Enums;
using Hubster.Direct.Interfaces;
using Hubster.Direct.Models;
using Hubster.Direct.Models.Direct;
using System;
using System.Collections.Generic;

namespace Hubster.Direct.Business.Activities
{
    /// <summary>
    /// 
    /// </summary>
    public class HubsterActivityBusiness : HubsterActivityBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HubsterActivityBusiness"/> class.
        /// </summary>
        /// <param name="directUrl">The direct URL.</param>
        internal HubsterActivityBusiness(string directUrl) : base(directUrl)
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
        /// <param name="activityModel">The activity model.</param>
        /// <returns></returns>
        public ApiResponse<DirectResponseModel> Send(IHubsterAuthorizer authorizer, Guid conversationId, DirectActivityModel activityModel)
        {
            var apiResponse = _engineAccess.Send(authorizer, conversationId, activityModel, "business");
            return apiResponse;
        }
    }
}
