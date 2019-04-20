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
    /// <seealso cref="Hubster.Direct.Interfaces.IHubsterDirectClientBusiness" />
    public class HubsterDirectClientBusiness : HubsterDirectClientBase, IHubsterDirectClientBusiness
    {
        public IHubsterConversationBusiness Conversation { get; private set; }
        public IHubsterActivityBusiness Activity { get; private set; }
        public IHubsterEventsBusiness Events { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HubsterDirectClientBusiness" /> class.
        /// </summary>
        /// <param name="directUrl">The host URL.</param>
        /// <param name="eventsUrl">The events URL.</param>
        public HubsterDirectClientBusiness(string directUrl = "https://direct.hubster.io", string eventsUrl = "https://events.hubster.io")
        {
            Conversation = new HubsterConversationBusiness(directUrl);
            Activity = new HubsterActivityBusiness(directUrl);
            Events = new HubsterEventsBusiness(eventsUrl);
        }
    }
}
