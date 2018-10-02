using BrokeredMessaging.Abstractions;
using BrokeredMessaging.Messaging.Features;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BrokeredMessaging.Messaging
{
    /// <summary>
    /// The default receive context which provides access to the received message and related
    /// functions using recieve features.
    /// </summary>
    public class DefaultReceiveContext : ReceiveContext
    {
        public DefaultReceiveContext()
        {

        }

        public DefaultReceiveContext(IFeatureCollection features)
        {

        }

        public override IFeatureCollection Features => throw new NotImplementedException();

        public override CancellationToken ReceiveAborted => throw new NotImplementedException();

        public override ReceivedMessage ReceivedMessage => throw new NotImplementedException();

        public override IServiceProvider RecieveServices => throw new NotImplementedException();

        public override Task AcknowledgeAsync()
        {
            throw new NotImplementedException();
        }

        public override Task RejectAsync(string reason)
        {
            throw new NotImplementedException();
        }

        public override Task RetryAsync(string reason)
        {
            throw new NotImplementedException();
        }
    }
}
