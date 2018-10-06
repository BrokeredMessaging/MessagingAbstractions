using System;
using System.Threading.Tasks;

namespace BrokeredMessaging.Messaging.Features
{
    internal class DeliveryAcknowledgementFeatureStub : IDeliveryAcknowledgementFeature
    {
        public bool Sent => false;

        public DeliveryStatus Status { get; set; }

        public string StatusReason { get; set; }

        public void OnCompleted(Func<Task, object> callback, object state)
        {
        }

        public void OnStarting(Func<Task, object> callback, object state)
        {
        }

        public Task SendAsync() => Task.CompletedTask;
    }
}
