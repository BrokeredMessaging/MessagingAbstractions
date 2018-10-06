using System;
using System.Threading.Tasks;

namespace BrokeredMessaging.Messaging
{
    /// <summary>
    /// Represents the delivery acknowlegement of a received message.
    /// </summary>
    public abstract class DeliveryAcknowledgement
    {
        /// <summary>
        /// Proviedes a mechanism to invoke callbacks without state.
        /// </summary>
        private static readonly Func<object, Task> CallbackInvoker = state => ((Func<Task>)state)();

        /// <summary>
        /// Gets the <see cref="ReceiveContext"/> for this acknowlegement.
        /// </summary>
        public abstract ReceiveContext ReceiveContext { get; }

        /// <summary>
        /// Gets whether the acknowledgement has been sent.
        /// </summary>
        public abstract bool Sent { get; }

        /// <summary>
        /// Gets or sets the delivery status to return to the broker.
        /// </summary>
        public abstract DeliveryStatus Status { get; set; }

        /// <summary>
        /// Gets or sets a status reason to return to the broker.
        /// </summary>
        public abstract string StatusReason { get; set; }

        /// <summary>
        /// Sends the acknowledgement to the broker.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public abstract Task SendAsync();

        /// <summary>
        /// Registers a callback to be invoked before the acknowledgement is sent.
        /// </summary>
        /// <param name="callback">The callback to invoke when starting the acknowledgement.</param>
        public virtual void OnStarting(Func<Task> callback) => OnStarting(CallbackInvoker, callback);

        /// <summary>
        /// Registers a callback to be invoked before the acknowledgement is sent.
        /// </summary>
        /// <param name="callback">The callback to invoke when starting the acknowledgement.</param>
        /// <param name="state">The state to pass to the callback.</param>
        public abstract void OnStarting(Func<Task, object> callback, object state);

        /// <summary>
        /// Registers a callback to be invoked after the acknowledgement is sent.
        /// </summary>
        /// <param name="callback">The callback to invoke after completing the acknowledgement.</param>
        public virtual void OnCompleted(Func<Task> callback) => OnCompleted(CallbackInvoker, callback);

        /// <summary>
        /// Registers a callback to be invoked after the acknowledgement is sent.
        /// </summary>
        /// <param name="callback">The callback to invoke after completing the acknowledgement.</param>
        /// <param name="state">The state to pass to the callback.</param>
        public abstract void OnCompleted(Func<Task, object> callback, object state);

        /// <summary>
        /// Sends a positive acknowlegement to the broker, accepting the message.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public virtual Task AcceptAsync()
        {
            Status = DeliveryStatus.Ok;
            return SendAsync();
        }

        /// <summary>
        /// Sends a negative acknowlegement to the broker, rejecting the message. The broker will treat the
        /// message as having failed and may forward it to a dead-letter queue for error processing.
        /// </summary>
        /// <param name="reason">The reason for the message being rejected.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public virtual Task RejectAsync(string reason = null)
        {
            Status = DeliveryStatus.Rejected;
            StatusReason = reason;
            return SendAsync();
        }

        /// <summary>
        /// Sends a negative acknowlegement to the broker, requesting the message is re-queued for another
        /// attempt a delivery. The broker may apply a redelivery policy to determine whether to attempt 
        /// redelivery or to treat the message has having failed.
        /// </summary>
        /// <param name="reason">The reason for the message being rejected.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public virtual Task RetryAsync(string reason = null)
        {
            Status = DeliveryStatus.Retry;
            StatusReason = reason;
            return SendAsync();
        }
    }
}