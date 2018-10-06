using System.Collections.Generic;
using System.IO;

namespace BrokeredMessaging.Messaging.Features
{
    /// <summary>
    /// Defines access properties of the message received from the broker.
    /// </summary>
    public interface IReceivedMessageFeature
    {
        /// <summary>
        /// Gets or sets the headers of the dictionary.
        /// </summary>
        IHeaderDictionary Headers { get; set; }

        /// <summary>
        /// Gets or sets the body of the message.
        /// </summary>
        Stream Body { get; set; }
    }
}
