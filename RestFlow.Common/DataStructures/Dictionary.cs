using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow.Common.DataStructures
{
    public class CustomDictionary<TKey, TValue>
    {
        private class KeyValuePair
        {
            public TKey Key;
            public TValue Value;

            public KeyValuePair(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }

        private const int InitialSize = 16;
        private LinkedList<KeyValuePair>[] buckets;

        public CustomDictionary()
        {
            buckets = new LinkedList<KeyValuePair>[InitialSize];
        }

        private int GetBucketIndex(TKey key)
        {
            int hash = key.GetHashCode();
            return Math.Abs(hash) % buckets.Length;
        }

        public void Add(TKey key, TValue value)
        {
            int index = GetBucketIndex(key);
            if (buckets[index] == null)
            {
                buckets[index] = new LinkedList<KeyValuePair>();
            }

            foreach (var pair in buckets[index])
            {
                if (pair.Key.Equals(key))
                {
                    throw new ArgumentException("Key already exists");
                }
            }

            buckets[index].AddLast(new KeyValuePair(key, value));
        }

        public TValue GetValue(TKey key)
        {
            int index = GetBucketIndex(key);
            if (buckets[index] != null)
            {
                foreach (var pair in buckets[index])
                {
                    if (pair.Key.Equals(key))
                    {
                        return pair.Value;
                    }
                }
            }
            throw new KeyNotFoundException("Key not found");
        }

        public void Remove(TKey key)
        {
            int index = GetBucketIndex(key);
            if (buckets[index] != null)
            {
                var current = buckets[index].First;
                while (current != null)
                {
                    if (current.Value.Key.Equals(key))
                    {
                        buckets[index].Remove(current);
                        return;
                    }
                    current = current.Next;
                }
            }
            throw new KeyNotFoundException("Key not found");
        }
    }

}
