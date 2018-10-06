using BrokeredMessaging.Abstractions;
using BrokeredMessaging.Messaging.Features;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BrokeredMessaging.Messaging
{
    /// <summary>
    /// The default receive context which provides access to the received message and related
    /// functions using recieve features.
    /// </summary>
    public class DefaultReceiveContext : ReceiveContext
    {
        private static readonly Func<IReceiveLifetimeFeature> ReceiveLifetimeFeatureFactory = ()
            => new ReceiveLifetimeFeatureStub();

        private static readonly Func<IServiceProvidersFeature> ServiceProvidersFeatureFactory = ()
            => new ServiceProvidersFeatureStub();

        private static readonly Func<IDeliveryAcknowledgementFeature> DeliveryAcknowledgementFeatureFactory = ()
            => new DeliveryAcknowledgementFeatureStub();

        private readonly ReceivedMessage _receivedMessage;

        private FeatureAccessor<ContextFeatures> _features;

        public DefaultReceiveContext()
            : this(new FeatureCollection())
        {
        }

        public DefaultReceiveContext(IFeatureCollection features)
        {
            if (features == null)
            {
                throw new ArgumentNullException(nameof(features));
            }

            _features = new FeatureAccessor<ContextFeatures>(features);
            _receivedMessage = new DefaultReceivedMessage(this);
        }

        public override IFeatureCollection Features => _features.Collection;

        private IReceiveLifetimeFeature ReceiveLifetimeFeature
            => _features.Get(ref _features.Cache.ReceiveLifetime, ReceiveLifetimeFeatureFactory);

        private IServiceProvidersFeature ServiceProvidersFeature
            => _features.Get(ref _features.Cache.ServiceProviders, ServiceProvidersFeatureFactory);

        private IDeliveryAcknowledgementFeature DeliveryAcknowledgementFeature
            => _features.Get(ref _features.Cache.DeliveryAcknowledgement, DeliveryAcknowledgementFeatureFactory);

        public override CancellationToken ReceiveAborted => ReceiveLifetimeFeature.ReceiveAborted;

        public override ReceivedMessage ReceivedMessage => _receivedMessage;

        public override IServiceProvider RecieveServices => ServiceProvidersFeature.ReceiveServices;

        public override Task AcknowledgeAsync()
        {
            DeliveryAcknowledgementFeature.Status = DeliveryStatus.Ok;
            return DeliveryAcknowledgementFeature.SendAsync();
        }

        public override Task RejectAsync(string reason)
        {
            DeliveryAcknowledgementFeature.Status = DeliveryStatus.Rejected;
            DeliveryAcknowledgementFeature.StatusReason = reason;
            return DeliveryAcknowledgementFeature.SendAsync();
        }

        public override Task RetryAsync(string reason)
        {
            DeliveryAcknowledgementFeature.Status = DeliveryStatus.Retry;
            DeliveryAcknowledgementFeature.StatusReason = reason;
            return DeliveryAcknowledgementFeature.SendAsync();
        }

        private struct ContextFeatures
        {
            public IReceiveLifetimeFeature ReceiveLifetime;

            public IServiceProvidersFeature ServiceProviders;

            public IDeliveryAcknowledgementFeature DeliveryAcknowledgement;
        }
    }
}
