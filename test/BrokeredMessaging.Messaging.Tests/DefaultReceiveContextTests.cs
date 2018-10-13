using BrokeredMessaging.Messaging.Features;
using Moq;
using System;
using System.IO;
using Xunit;

namespace BrokeredMessaging.Messaging.Tests
{
    public class DefaultReceiveContextTests
    {
        [Fact]
        public void RequestProperties_MapToRequestMessageFeature()
        {
            var context = new DefaultReceiveContext();

            var feature = new TestReceivedMessageFeature();

            context.Features.Set<IReceivedMessageFeature>(feature);

            feature.Headers = new HeaderDictionary();
            feature.Body = Stream.Null;
            feature.Source = "test";

            var message = context.ReceivedMessage;

            Assert.Equal(feature.Headers, message.Headers);
            Assert.Equal(feature.Body, message.Body);
            Assert.Equal(feature.Source, message.Source);
        }

        [Fact]
        public void ContextProperties_MapToReceiveLifetimeFeature()
        {
            var context = new DefaultReceiveContext();

            var feature = new TestReceiveLifetimeFeature();

            context.Features.Set<IReceiveLifetimeFeature>(feature);

            Assert.False(context.ReceiveAborted.IsCancellationRequested);

            feature.Abort();

            Assert.True(context.ReceiveAborted.IsCancellationRequested);
        }

        [Fact]
        public void ContextProperties_MapToServiceProvidersFeature()
        {
            var context = new DefaultReceiveContext();

            var feature = new TestServiceProvidersFeature();

            context.Features.Set<IServiceProvidersFeature>(feature);

            Assert.Null(context.RecieveServices);

            feature.ReceiveServices = Mock.Of<IServiceProvider>();

            Assert.Equal(feature.ReceiveServices, context.RecieveServices);
        }

        [Fact]
        public void DeliveryAcknowledgementProperties_MapToDeliveryAcknowledgementProperties()
        {
            var context = new DefaultReceiveContext();

            var feature = new TestDeliveryAcknowledgementFeature();

            context.Features.Set<IDeliveryAcknowledgementFeature>(feature);

            Assert.Equal(feature.Sent, context.DeliveryAcknowledgement.Sent);
            Assert.Equal(feature.Status, context.DeliveryAcknowledgement.Status);
            Assert.Equal(feature.StatusReason, context.DeliveryAcknowledgement.StatusReason);

            feature.Sent = true;
            feature.Status = DeliveryStatus.Rejected;
            feature.StatusReason = "fibble";

            Assert.Equal(feature.Sent, context.DeliveryAcknowledgement.Sent);
            Assert.Equal(feature.Status, context.DeliveryAcknowledgement.Status);
            Assert.Equal(feature.StatusReason, context.DeliveryAcknowledgement.StatusReason);
        }

        [Fact]
        public void SendDeliveryAcknowlegementAsync_InvokesDeliveryFeature()
        {
            var context = new DefaultReceiveContext();

            var featureMock = new Mock<IDeliveryAcknowledgementFeature>();

            context.Features.Set(featureMock.Object);

            context.DeliveryAcknowledgement.SendAsync();

            featureMock.Verify(o => o.SendAsync());
        }
    }
}
