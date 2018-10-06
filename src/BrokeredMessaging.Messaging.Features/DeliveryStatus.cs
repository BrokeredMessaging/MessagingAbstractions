namespace BrokeredMessaging.Messaging
{
    /// <summary>
    /// Specifies the status of a message's delivery.
    /// </summary>
    public enum DeliveryStatus
    {
        /// <summary>
        /// The message has been delivered successfully. The broker can stop tracking the message.
        /// </summary>
        Ok,

        /// <summary>
        /// The message has not been delivered, but the broker may make an attempt to deliver the message again.
        /// </summary>
        Retry,

        /// <summary>
        /// The message has not been delivered and the broker should not attempt to deliver it again.
        /// Messages with this status are typically sent to a dead-letter queue.
        /// </summary>
        Rejected
    }
}
