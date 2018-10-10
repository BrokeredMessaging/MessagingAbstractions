using BrokeredMessaging.Messaging.Features;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrokeredMessaging.Messaging
{
    /// <summary>
    /// Defines a mechanism for building a message-receiving pipeline.
    /// </summary>
    public interface IReceiverBuilder
    {
        /// <summary>
        /// Gets the collection of services available to the receiver.
        /// </summary>
        IServiceProvider ReceiverServices { get; }

        /// <summary>
        /// Gets the collection of features made available to the receiver.
        /// </summary>
        IFeatureCollection Features { get; }

        /// <summary>
        /// Gets a key/value which can be used to share information between middleware. 
        /// </summary>
        IDictionary<string, object> Properties { get; }

        /// <summary>
        /// Adds a middlewear delegate to the message-receiver pipeline.
        /// </summary>
        /// <param name="middlewear">The middlewear delegate to add to the builder.</param>
        /// <returns>The <see cref="IReceiverBuilder"/>.</returns>
        IReceiverBuilder Use(Func<ReceiveDelegate, ReceiveDelegate> middlewear);

        /// <summary>
        /// Builds the message-receiver pipeline delegate.
        /// </summary>
        /// <returns>The message-receiving delegate.</returns>
        ReceiveDelegate Build();
    }
}
