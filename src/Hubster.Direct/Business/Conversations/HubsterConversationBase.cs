// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Direct.RemoteAccess;

namespace Hubster.Direct.Business.Conversations
{
    /// <summary>
    /// 
    /// </summary>
    public class HubsterConversationBase
    {
        internal readonly EngineConversationAccess _engineAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="HubsterConversation" /> class.
        /// </summary>
        /// <param name="directUrl">The host URL.</param>
        internal HubsterConversationBase(string directUrl)
        {            
            _engineAccess = new EngineConversationAccess(directUrl);
        }
    }
}
