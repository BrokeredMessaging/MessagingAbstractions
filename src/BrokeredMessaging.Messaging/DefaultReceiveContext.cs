using BrokeredMessaging.Messaging.Features;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BrokeredMessaging.Messaging
{
    /// <summary>
    /// The default receive context which provides access to the received message and related
    /// functions using listener features.
    /// </summary>
    public class DefaultReceiveContext : ReceiveContext
    {
        private static readonly Func<IReceiveLifetimeFeature> ReceiveLifetimeFeatureFactory = ()
            => new ReceiveLifetimeFeatureStub();

        private static readonly Func<IServiceProvidersFeature> ServiceProvidersFeatureFactory = ()
            => new ServiceProvidersFeatureStub();

        private readonly ReceivedMessage _receivedMessage;

        private readonly DefaultDeliveryAcknowlegement _deliveryAcknowedgement;

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
            _deliveryAcknowedgement = new DefaultDeliveryAcknowlegement(this);
        }

        public override IFeatureCollection Features => _features.Collection;

        private IReceiveLifetimeFeature ReceiveLifetimeFeature
            => _features.Get(ref _features.Cache.ReceiveLifetime, ReceiveLifetimeFeatureFactory);

        private IServiceProvidersFeature ServiceProvidersFeature
            => _features.Get(ref _features.Cache.ServiceProviders, ServiceProvidersFeatureFactory);

        public override CancellationToken ReceiveAborted => ReceiveLifetimeFeature.ReceiveAborted;

        public override ReceivedMessage ReceivedMessage => _receivedMessage;

        public override DeliveryAcknowledgement DeliveryAcknowledgement => _deliveryAcknowedgement;

        public override IServiceProvider RecieveServices => ServiceProvidersFeature.ReceiveServices;

        private struct ContextFeatures
        {
            public IReceiveLifetimeFeature ReceiveLifetime;

            public IServiceProvidersFeature ServiceProviders;
        }
    }

    /// <summary>
    /// The default delivery acknowledgement implementation, using listener features to perform 
    /// delivery acknowledgment.
    /// </summary>
    public class DefaultDeliveryAcknowlegement : DeliveryAcknowledgement
    {
        private static readonly Func<IDeliveryAcknowledgementFeature> DeliveryAcknowledgementFeatureFactory = ()
            => new DeliveryAcknowledgementFeatureStub();

        private FeatureAccessor<AcknowledgementFeatures> _features;

        public DefaultDeliveryAcknowlegement(ReceiveContext receiveContext)
        {
            ReceiveContext = receiveContext ?? throw new ArgumentNullException(nameof(receiveContext));
            _features = new FeatureAccessor<AcknowledgementFeatures>(receiveContext.Features);
        }

        private IDeliveryAcknowledgementFeature DeliveryAcknowledgementFeature
            => _features.Get(ref _features.Cache.DeliveryAcknowledgement, DeliveryAcknowledgementFeatureFactory);

        public override ReceiveContext ReceiveContext { get; }

        public override bool Sent => DeliveryAcknowledgementFeature.Sent;

        public override DeliveryStatus Status
        {
            get => DeliveryAcknowledgementFeature.Status;
            set => DeliveryAcknowledgementFeature.Status = value;
        }

        public override string StatusReason
        {
            get => DeliveryAcknowledgementFeature.StatusReason;
            set => DeliveryAcknowledgementFeature.StatusReason = value;
        }

        public override void OnCompleted(Func<Task, object> callback, object state) 
            => DeliveryAcknowledgementFeature.OnCompleted(callback, state);

        public override void OnStarting(Func<Task, object> callback, object state)
            => DeliveryAcknowledgementFeature.OnStarting(callback, state);

        public override Task SendAsync()
            => DeliveryAcknowledgementFeature.SendAsync();

        private struct AcknowledgementFeatures
        {
            public IDeliveryAcknowledgementFeature DeliveryAcknowledgement;
        }
    }
}
