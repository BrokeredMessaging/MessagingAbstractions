using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BrokeredMessaging.Messaging.Features
{
    /// <summary>
    /// Provides access to the lifetime of a receive operation.
    /// </summary>
    public interface IReceiveLifetimeFeature
    {
        /// <summary>
        /// Gets a cancellation token which signals if the receive operation is aborted. This will
        /// most likely be in response to a loss of communication with the message broker.
        /// </summary>
        CancellationToken ReceiveAborted { get; }
    }
}
