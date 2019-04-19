using Hubster.Direct.Business.Activities;
using Hubster.Direct.Business.Conversations;
using Hubster.Direct.Events;

namespace Hubster.Direct
{
    /// <summary>
    /// 
    /// </summary>
    public class HubsterDirectClientCustomer : HubsterDirectClientBase
    {
        public HubsterConversationCustomer Converstion { get; private set; }
        public HubsterActivityCustomer Activity { get; private set; }
        public HubsterEventsCustomer Events { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HubsterDirectClientCustomer" /> class.
        /// </summary>
        /// <param name="directUrl">The host URL.</param>
        /// <param name="eventsUrl">The events URL.</param>
        public HubsterDirectClientCustomer(string directUrl = "https://direct.hubster.io", string eventsUrl = "https://events.hubster.io")
        {
            Converstion = new HubsterConversationCustomer(directUrl);
            Activity = new HubsterActivityCustomer(directUrl);
            Events = new HubsterEventsCustomer(eventsUrl);
        }
    }
}
