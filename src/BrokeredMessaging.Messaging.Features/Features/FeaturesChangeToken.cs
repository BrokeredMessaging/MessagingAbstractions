using Microsoft.Extensions.Primitives;
using System;

namespace BrokeredMessaging.Messaging.Features
{
    public struct FeaturesChangeToken : IChangeToken
    {
        private readonly FeaturesChangeTokenSource _tokenSource;

        public static FeaturesChangeToken None { get; } = new FeaturesChangeToken();

        public bool HasChanged => _tokenSource != null && _tokenSource.HasChanged;

        public bool ActiveChangeCallbacks => false;

        internal FeaturesChangeToken(FeaturesChangeTokenSource tokenSource)
        {
            _tokenSource = tokenSource;
        }

        public IDisposable RegisterChangeCallback(Action<object> callback, object state) 
            => throw new NotSupportedException();
    }
}
