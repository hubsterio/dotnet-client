using Hubster.Direct.Business.Activities;
using Hubster.Direct.Business.Conversations;
using Hubster.Direct.Events;

namespace Hubster.Direct
{
    /// <summary>
    /// 
    /// </summary>
    public class HubsterDirectClientBusiness : HubsterDirectClientBase
    {
        public HubsterConversationBusiness Converstion { get; private set; }
        public HubsterActivityBusiness Activity { get; private set; }
        public HubsterEventsBusiness Events { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HubsterDirectClientBusiness" /> class.
        /// </summary>
        /// <param name="directUrl">The host URL.</param>
        /// <param name="eventsUrl">The events URL.</param>
        public HubsterDirectClientBusiness(string directUrl = "https://direct.hubster.io", string eventsUrl = "https://events.hubster.io")
        {
            Converstion = new HubsterConversationBusiness(directUrl);
            Activity = new HubsterActivityBusiness(directUrl);
            Events = new HubsterEventsBusiness(eventsUrl);
        }
    }
}
