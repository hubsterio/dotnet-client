// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Direct.Interfaces;

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
    }
}
