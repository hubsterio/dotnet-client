﻿using Hubster.Direct.Interfaces;
using Hubster.Direct.Models.Direct;
using System;

namespace Hubster.Direct.Models
{
    public class StartOptions
    {
        public IHubsterAuthorizer Authorizer { get; set; }
        public Guid IntegrationId { get; set; }
        public Guid? ConversationId { get; set; }
        public Action OnConnected { get; set; }
        public Action OnDisconnected { get; set; }
        public Action<DirectActivityModel> OnActivity { get; set; }
        public Action<ErrorCodeModel> OnError { get; set; }
    }
}
