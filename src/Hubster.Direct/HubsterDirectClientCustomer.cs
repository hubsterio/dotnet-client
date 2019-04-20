using Hubster.Direct.Business.Activities;
using Hubster.Direct.Business.Conversations;
using Hubster.Direct.Events;
using Hubster.Direct.Interfaces;

namespace Hubster.Direct
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Hubster.Direct.HubsterDirectClientBase" />
    /// <seealso cref="Hubster.Direct.Interfaces.IHubsterDirectClientCustomer" />
    public class HubsterDirectClientCustomer : HubsterDirectClientBase, IHubsterDirectClientCustomer
    {
        public IHubsterConversationCustomer Conversation { get; private set; }
        public IHubsterActivityCustomer Activity { get; private set; }
        public IHubsterEventsCustomer Events { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HubsterDirectClientCustomer" /> class.
        /// </summary>
        /// <param name="directUrl">The host URL.</param>
        /// <param name="eventsUrl">The events URL.</param>
        public HubsterDirectClientCustomer(string directUrl = "https://direct.hubster.io", string eventsUrl = "https://events.hubster.io")
        {
            Conversation = new HubsterConversationCustomer(directUrl);
            Activity = new HubsterActivityCustomer(directUrl);
            Events = new HubsterEventsCustomer(eventsUrl);
        }
    }
}
