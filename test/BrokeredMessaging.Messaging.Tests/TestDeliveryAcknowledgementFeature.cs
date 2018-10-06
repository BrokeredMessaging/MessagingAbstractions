using System;
using System.Threading.Tasks;
using BrokeredMessaging.Messaging.Features;

namespace BrokeredMessaging.Messaging.Tests
{
    public class TestDeliveryAcknowledgementFeature : IDeliveryAcknowledgementFeature
    {
        public bool Sent { get; set; }

        public DeliveryStatus Status { get; set; }

        public string StatusReason { get; set; }

        public void OnCompleted(Func<Task, object> callback, object state)
        {
            throw new NotImplementedException();
        }

        public void OnStarting(Func<Task, object> callback, object state)
        {
            throw new NotImplementedException();
        }

        public Task SendAsync()
        {
            throw new NotImplementedException();
        }
    }
}