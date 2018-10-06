using System;

namespace BrokeredMessaging.Messaging.Features
{
    internal class ServiceProvidersFeatureStub : IServiceProvidersFeature
    {
        public IServiceProvider ReceiveServices { get; set; }
    }
}
