using System;

namespace BrokeredMessaging.Messaging.Features
{
    public struct FeatureAccessor<TCache> where TCache : struct
    {
        private FeaturesChangeToken _changeToken;

        public FeatureAccessor(IFeatureCollection collection)
        {
            Collection = collection ?? throw new ArgumentNullException(nameof(collection));
            _changeToken = collection.GetChangeToken();
            Cache = default;
        }

        public TCache Cache;

        public IFeatureCollection Collection { get; }

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
            feature = Collection.Get<TFeature>();

            if (feature == null)
            {
                // If the feature doesn't exist in the collection, initialize it now.
                feature = featureFactory();
                Collection.Set(feature);

                // We changed the collection; save the new change token.
                _changeToken = Collection.GetChangeToken();
            }
            else if (flush)
            {
                // If the cache has been flushed, update the change token.
                _changeToken = Collection.GetChangeToken();
            }

            return feature;
        }
    }
}
