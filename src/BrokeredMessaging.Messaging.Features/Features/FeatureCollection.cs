using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Primitives;

namespace BrokeredMessaging.Messaging.Features
{
    public class FeatureCollection : IFeatureCollection
    {
        private readonly Dictionary<Type, object> _features = new Dictionary<Type, object>();

        private FeaturesChangeTokenSource _cts = new FeaturesChangeTokenSource();

        public int Count => _features.Count;

        bool ICollection<KeyValuePair<Type, object>>.IsReadOnly => false;

        void ICollection<KeyValuePair<Type, object>>.Add(KeyValuePair<Type, object> item)
        {
            ((ICollection<KeyValuePair<Type, object>>)_features).Add(item);
            OnChanged();
        }

        public void Clear()
        {
            _features.Clear();
            OnChanged();
        }

        bool ICollection<KeyValuePair<Type, object>>.Contains(KeyValuePair<Type, object> item) 
            => ((ICollection<KeyValuePair<Type, object>>)_features).Contains(item);

        void ICollection<KeyValuePair<Type, object>>.CopyTo(KeyValuePair<Type, object>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<Type, object>>)_features).CopyTo(array, arrayIndex);
        }

        public TFeature Get<TFeature>() where TFeature : class
        {
            var key = typeof(TFeature);
            if (_features.TryGetValue(key, out var feature))
            {
                return (TFeature)feature;
            }

            return default;
        }

        public FeaturesChangeToken GetChangeToken() => _cts.Token;

        public IEnumerator<KeyValuePair<Type, object>> GetEnumerator() => _features.GetEnumerator();

        bool ICollection<KeyValuePair<Type, object>>.Remove(KeyValuePair<Type, object> item)
        {
            var removed = ((ICollection<KeyValuePair<Type, object>>)_features).Remove(item);

            if (removed)
            {
                OnChanged();
            }

            return removed;
        }

        public void Set<TFeature>(TFeature feature) where TFeature : class
        {
            var key = typeof(TFeature);
            if (feature == null)
            {
                if (_features.Remove(key))
                {
                    OnChanged();
                }
            }
            else
            {
                _features[key] = feature;
                OnChanged();
            }
        }

        private void OnChanged()
        {
            _cts.Changed();
            _cts = new FeaturesChangeTokenSource();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
