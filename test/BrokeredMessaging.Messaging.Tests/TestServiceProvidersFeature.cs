using System;
using BrokeredMessaging.Messaging.Features;

namespace BrokeredMessaging.Messaging.Tests
{
    public class TestServiceProvidersFeature : IServiceProvidersFeature
    {
        public IServiceProvider ReceiveServices { get; set; }
    }
}