using System;

namespace BrokeredMessaging.Messaging.Features
{
    /// <summary>
    /// Defines access to the services available in the scope of a receive operation.
    /// </summary>
    public interface IServiceProvidersFeature
    {
        /// <summary>
        /// Gets or sets the service provider for the receive operation.
        /// </summary>
        IServiceProvider ReceiveServices { get; set; }
    }
}
