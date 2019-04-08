using Hubster.Auth;

namespace Hubster.Direct
{
    /// <summary>
    /// 
    /// </summary>
    public class HubsterDirectClient
    {
        public HubsterConversation Conversation { get; private set; }        
        public HubsterActivity Activity { get; private set; }
        public HubsterResource Resource { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HubsterDirectClient"/> class.
        /// </summary>
        /// <param name="authClient">The authentication client.</param>
        /// <param name="hostUrl">The host URL.</param>
        public HubsterDirectClient(HubsterAuthClient authClient, string hostUrl = "https://direct.hubster.io")
        {
            Conversation = new HubsterConversation(authClient, hostUrl);
            Activity = new HubsterActivity(authClient, hostUrl);
            Resource = new HubsterResource(authClient, hostUrl);
        }
    }
}
