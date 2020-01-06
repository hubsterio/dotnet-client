// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Direct.RemoteAccess;

namespace Hubster.Direct.Business.Resources
{
    /// <summary>
    /// 
    /// </summary>
    public class HubsterResource
    {
        private readonly EngineResourceAccess _engineAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="HubsterResource" /> class.
        /// </summary>
        /// <param name="directUrl">The host URL.</param>
        internal HubsterResource(string directUrl)
        {            
            _engineAccess = new EngineResourceAccess(directUrl);
        }
    }
}
