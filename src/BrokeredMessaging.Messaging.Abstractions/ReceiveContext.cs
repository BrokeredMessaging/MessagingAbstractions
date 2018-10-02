using BrokeredMessaging.Messaging.Features;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BrokeredMessaging.Abstractions
{
    public abstract class ReceiveContext
    {
        public abstract IFeatureCollection Features { get; }

        public abstract CancellationToken ReceiveAborted { get; }

        public abstract ReceivedMessage ReceivedMessage { get; }

        public abstract IServiceProvider RecieveServices { get; }

        public abstract Task AcknowledgeAsync();

        public abstract Task RejectAsync(string reason = null);

        public abstract Task RetryAsync(string reason = null);
    }
}