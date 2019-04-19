using Hubster.Direct.Business.Resources;

namespace Hubster.Direct
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class HubsterDirectClientBase
    {
        public HubsterResource Resource { get; private set; }        

        /// <summary>
        /// Initializes a new instance of the <see cref="HubsterDirectClientBase" /> class.
        /// </summary>
        /// <param name="directUrl">The host URL.</param>
        /// <param name="eventsUrl">The events URL.</param>
        public HubsterDirectClientBase(string directUrl = "https://direct.hubster.io", string eventsUrl = "https://events.hubster.io")
        {
            Resource = new HubsterResource(directUrl);
        }
    }
}
