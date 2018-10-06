using System;
using System.IO;
using BrokeredMessaging.Abstractions;
using BrokeredMessaging.Messaging.Features;

namespace BrokeredMessaging.Messaging
{
    public class DefaultReceivedMessage : ReceivedMessage
    {
        private static readonly Func<IReceivedMessageFeature> ReceivedMessageFeatureFactory = ()
            => new ReceivedMessageFeatureStub();

        private FeatureAccessor<ReceivedMessageFeatures> _features;

        public DefaultReceivedMessage(ReceiveContext receiveContext)
        {
            ReceiveContext = receiveContext ?? throw new ArgumentNullException(nameof(receiveContext));
            _features = new FeatureAccessor<ReceivedMessageFeatures>(receiveContext.Features);
        }

        private IReceivedMessageFeature ReceivedMessageFeature
            => _features.Get(ref _features.Cache.ReceivedMessage, ReceivedMessageFeatureFactory);

        public override ReceiveContext ReceiveContext { get; }

        public override IHeaderDictionary Headers => ReceivedMessageFeature.Headers;

        public override Stream Body
        {
            get => ReceivedMessageFeature.Body;
            set => ReceivedMessageFeature.Body = value;
        }

        private struct ReceivedMessageFeatures
        {
            public IReceivedMessageFeature ReceivedMessage;
        }
    }
}