// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Direct.RemoteAccess;

namespace Hubster.Direct.Business.Activities
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class HubsterActivityBase
    {
        internal readonly EngineActivityAccess _engineAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="HubsterActivityBase" /> class.
        /// </summary>
        /// <param name="directUrl">The host URL.</param>
        internal HubsterActivityBase(string origin, string directUrl)
        {            
            _engineAccess = new EngineActivityAccess(origin, directUrl);
        }
    }
}
