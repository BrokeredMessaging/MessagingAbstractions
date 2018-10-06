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

            var message = context.ReceivedMessage;

            Assert.Equal(feature.Headers, message.Headers);
            Assert.Equal(feature.Body, message.Body);
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
    }
}
