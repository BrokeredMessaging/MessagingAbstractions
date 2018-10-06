using System.Threading;
using BrokeredMessaging.Messaging.Features;

namespace BrokeredMessaging.Messaging.Tests
{
    public class TestReceiveLifetimeFeature : IReceiveLifetimeFeature
    {
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public CancellationToken ReceiveAborted => _cts.Token;

        public void Abort() => _cts.Cancel();
    }
}