using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Primitives;

namespace BrokeredMessaging.Messaging
{
    /// <summary>
    /// Represents a collection of message headers.
    /// </summary>
    public class HeaderDictionary : IHeaderDictionary
    {
        private readonly Dictionary<string, StringValues> _headers = new Dictionary<string, StringValues>();

        public StringValues this[string key]
        {
            get
            {
                if (_headers.TryGetValue(key, out var value))
                {
                    return value;
                }

                return StringValues.Empty;
            }

            set
            {
                _headers[key] = value;
            }
        }

        public ICollection<string> Keys => _headers.Keys;

        public ICollection<StringValues> Values => _headers.Values;

        public int Count => _headers.Count;

        bool ICollection<KeyValuePair<string, StringValues>>.IsReadOnly => false;

        public void Add(string key, StringValues value) => _headers.Add(key, value);

        void ICollection<KeyValuePair<string, StringValues>>.Add(KeyValuePair<string, StringValues> item) 
            => Add(item.Key, item.Value);

        public void Clear() => _headers.Clear();

        bool ICollection<KeyValuePair<string, StringValues>>.Contains(KeyValuePair<string, StringValues> item)
            => this[item.Key] == item.Value;

        public bool ContainsKey(string key) => _headers.ContainsKey(key);

        void ICollection<KeyValuePair<string, StringValues>>.CopyTo(
            KeyValuePair<string, StringValues>[] array,
            int arrayIndex)
            => ((ICollection<KeyValuePair<string, StringValues>>)_headers).CopyTo(array, arrayIndex);

        public IEnumerator<KeyValuePair<string, StringValues>> GetEnumerator() => _headers.GetEnumerator();

        public bool Remove(string key) => _headers.Remove(key);

        bool ICollection<KeyValuePair<string, StringValues>>.Remove(KeyValuePair<string, StringValues> item)
            => ((ICollection<KeyValuePair<string, StringValues>>)_headers).Remove(item);

        public bool TryGetValue(string key, out StringValues value)
            => _headers.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
