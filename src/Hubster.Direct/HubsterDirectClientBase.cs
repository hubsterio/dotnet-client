// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

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
        /// <param name="origin">The origin.</param>
        /// <param name="directUrl">The host URL.</param>
        public HubsterDirectClientBase(string origin, string directUrl = "https://direct.hubster.io")
        {
            Resource = new HubsterResource(origin, directUrl);
        }
    }
}
