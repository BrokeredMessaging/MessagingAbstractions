using System.IO;

namespace BrokeredMessaging.Messaging
{
    /// <summary>
    /// Represents a message received from a message broker.
    /// </summary>
    public abstract class ReceivedMessage
    {
        /// <summary>
        /// Gets the receive context for this message.
        /// </summary>
        public abstract ReceiveContext ReceiveContext { get; }

        /// <summary>
        /// Gets the headers of the message.
        /// </summary>
        public abstract IHeaderDictionary Headers { get; }

        /// <summary>
        /// Gets or sets the body of the message.
        /// </summary>
        public abstract Stream Body { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the container the message was received from.
        /// </summary>
        public abstract string Source { get; set; }
    }
}
