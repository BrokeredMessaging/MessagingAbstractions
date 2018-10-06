using System;

namespace BrokeredMessaging.Messaging.Features
{
    public struct FeatureAccessor<TCache> where TCache : struct
    {
        private readonly IFeatureCollection _features;

        private FeaturesChangeToken _changeToken;

        public FeatureAccessor(IFeatureCollection features)
        {
            _features = features ?? throw new ArgumentNullException(nameof(features));
            _changeToken = features.GetChangeToken();
            Cache = default;
        }

        public TCache Cache;
        internal IReceivedMessageFeature ReceivedMessage;

        public TFeature Get<TFeature>(ref TFeature feature, Func<TFeature> featureFactory)
            where TFeature : class
        {
            var flush = false;
            if (_changeToken.HasChanged)
            {
                flush = true;
            }

            return feature ?? UpdateCache(ref feature, featureFactory, flush);
        }

        private TFeature UpdateCache<TFeature>(ref TFeature feature, Func<TFeature> featureFactory, bool flush)
            where TFeature : class
        {
            if (flush)
            {
                // Clear cache fields.
                Cache = default;
            }

            // Update the cache from the features collection.
            feature = _features.Get<TFeature>();

            if (feature == null)
            {
                // If the feature doesn't exist in the collection, initialize it now.
                feature = featureFactory();
                _features.Set(feature);

                // We changed the collection; save the new change token.
                _changeToken = _features.GetChangeToken();
            }
            else if (flush)
            {
                // If the cache has been flushed, update the change token.
                _changeToken = _features.GetChangeToken();
            }

            return feature;
        }
    }
}
