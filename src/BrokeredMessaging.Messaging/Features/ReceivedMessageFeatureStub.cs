using System.IO;

namespace BrokeredMessaging.Messaging.Features
{
    /// <summary>
    /// A stub implementation of the <see cref="IReceivedMessageFeature"/> interface.
    /// </summary>
    internal class ReceivedMessageFeatureStub : IReceivedMessageFeature
    {
        public IHeaderDictionary Headers { get; set; } = new HeaderDictionary();

        public Stream Body { get; set; } = Stream.Null;

        public string Source { get; set; }
    }
}
