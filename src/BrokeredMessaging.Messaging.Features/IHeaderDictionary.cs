using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrokeredMessaging.Messaging
{
    /// <summary>
    /// Represents the headers of a message.
    /// </summary>
    public interface IHeaderDictionary : IDictionary<string, StringValues>
    {
    }
}
