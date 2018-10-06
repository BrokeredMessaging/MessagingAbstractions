using BrokeredMessaging.Messaging.Features;
using System;
using System.Threading;

namespace BrokeredMessaging.Messaging
{
    /// <summary>
    /// Represents the context of receving a message from a broker.
    /// </summary>
    public abstract class ReceiveContext
    {
        /// <summary>
        /// Gets the collection of listener features used by the context.
        /// </summary>
        public abstract IFeatureCollection Features { get; }

        /// <summary>
        /// Gets a token which signals when the receive has been aborted. Typically this is an indication
        /// that connectivity with the broker has been lost.
        /// </summary>
        public abstract CancellationToken ReceiveAborted { get; }

        /// <summary>
        /// Gets the message received from the broker.
        /// </summary>
        public abstract ReceivedMessage ReceivedMessage { get; }

        /// <summary>
        /// Gets the service provider for the receive context.
        /// </summary>
        public abstract IServiceProvider RecieveServices { get; }

        /// <summary>
        /// Gets the delivery acknowlegement for the recieve of the message.
        /// </summary>
        public abstract DeliveryAcknowledgement DeliveryAcknowledgement { get; }
    }
}