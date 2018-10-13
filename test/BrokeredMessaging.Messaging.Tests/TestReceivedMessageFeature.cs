using System.IO;
using BrokeredMessaging.Messaging.Features;

namespace BrokeredMessaging.Messaging.Tests
{
    public class TestReceivedMessageFeature : IReceivedMessageFeature
    {
        public TestReceivedMessageFeature()
        {
        }

        public IHeaderDictionary Headers { get; set; }

        public Stream Body { get; set; }

        public string Source { get; set; }
    }
}