using System;
using System.Collections.Generic;

namespace BrokeredMessaging.Messaging.Features
{
    /// <summary>
    /// Represents a collection of messaging features.
    /// </summary>
    public interface IFeatureCollection : ICollection<KeyValuePair<Type, object>>
    {
        /// <summary>
        /// Returns a change token which indicates when the collection of features is changed.
        /// </summary>
        /// <returns>An <see cref="FeaturesChangeToken"/> for the collection.</returns>
        FeaturesChangeToken GetChangeToken();

        /// <summary>
        /// Gets a feature from the collection.
        /// </summary>
        /// <typeparam name="TFeature">The type of feature to get.</typeparam>
        /// <returns>The feature from the collection, or <c>null</c> if the feature
        /// does not exist.</returns>
        TFeature Get<TFeature>() where TFeature : class;

        /// <summary>
        /// Sets a feature in the collection.
        /// </summary>
        /// <typeparam name="TFeature">The type of feature being set.</typeparam>
        /// <param name="feature">The feature to set.</param>
        void Set<TFeature>(TFeature feature) where TFeature : class;
    }
}
