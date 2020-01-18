// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Direct.Interfaces;

namespace Hubster.Direct.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class HubsterEventsCustomer : HubsterEventsBase, IHubsterEventsCustomer
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="HubsterEventsCustomer" /> class.
        /// </summary>
        /// <param name="eventsUrl">The host URL.</param>
        internal HubsterEventsCustomer(string eventsUrl) : base(eventsUrl)
        {
        }
    }
}
