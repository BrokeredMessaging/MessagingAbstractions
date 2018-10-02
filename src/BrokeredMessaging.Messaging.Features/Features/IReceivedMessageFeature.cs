using System.Collections.Generic;
using System.IO;

namespace BrokeredMessaging.Messaging.Features
{
    public interface IReceivedMessageFeature
    {
        ICollection<KeyValuePair<string, string>> Headers { get; }

        Stream ReadBody();
    }
}
