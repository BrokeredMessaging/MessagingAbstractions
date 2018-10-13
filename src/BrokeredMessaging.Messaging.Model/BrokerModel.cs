using System;
using System.Collections.Generic;
using System.Text;

namespace BrokeredMessaging.Messaging.Model
{
    /// <summary>
    /// A model of a message-broker 
    /// </summary>
    public class BrokerModel
    {
        /// <summary>
        /// Gets the queues exposed by the broker.
        /// </summary>
        public ICollection<QueueModel> Queues { get; } = new List<QueueModel>();
    }
}
