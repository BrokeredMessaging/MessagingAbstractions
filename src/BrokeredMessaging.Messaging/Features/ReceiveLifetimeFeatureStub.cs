using System.Threading;

namespace BrokeredMessaging.Messaging.Features
{
    internal class ReceiveLifetimeFeatureStub : IReceiveLifetimeFeature
    {
        public CancellationToken ReceiveAborted { get; set; }
    }
}
