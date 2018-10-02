using System;
using System.Threading.Tasks;

namespace BrokeredMessaging.Messaging.Features
{
    /// <summary>
    /// Defines the mechanism for acknowledging delivery (or rejection) of messages.
    /// </summary>
    public interface IDeliveryAcknowledgementFeature
    {
        /// <summary>
        /// Gets whether an ackmowledgement has been sent.
        /// </summary>
        bool Sent { get; }

        /// <summary>
        /// Gets or sets the status of the message delivery.
        /// </summary>
        DeliveryStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the reason for the current <see cref="Status"/> value.
        /// </summary>
        string StatusReason { get; set; }

        /// <summary>
        /// Sends the acknowledgement to the broker.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task SendAsync();

        /// <summary>
        /// Registers a callback to be invoked before the acknowledgement is sent.
        /// </summary>
        /// <param name="callback">The callback to invoke when starting the acknowledgement.</param>
        /// <param name="state">The state to pass to the callback.</param>
        void OnStarting(Func<Task, object> callback, object state);

        /// <summary>
        /// Registers a callback to be invoked after the acknowledgement is sent.
        /// </summary>
        /// <param name="callback">The callback to invoke after completing the acknowledgement.</param>
        /// <param name="state">The state to pass to the callback.</param>
        void OnCompleted(Func<Task, object> callback, object state);
    }
}
