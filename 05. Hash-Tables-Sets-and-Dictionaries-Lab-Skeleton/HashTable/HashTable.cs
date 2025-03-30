namespace HashTable
{
    using System;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;

    public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
    {
        private const float LoadFactor = 0.75f;
        private const int DefaultCapacity = 4;

        private LinkedList<KeyValue<TKey, TValue>>[] slots;

        public int Count { get; private set; }

        public int Capacity => this.slots.Length;

        public HashTable()
            : this(DefaultCapacity) { }

        public HashTable(int capacity)
        {
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[capacity];
        }

        private HashTable(int capacity, IEnumerable<KeyValue<TKey, TValue>> keyValuePairs)
            : this(capacity)
        {
            foreach (var element in keyValuePairs)
            {
                this.Add(element.Key, element.Value);
            }
        }

        public void Add(TKey key, TValue value)
        {
            this.GrowIfNeeded();

            int index = Math.Abs(key.GetHashCode()) % this.Capacity;

            if (this.slots[index] == null)
            {
                this.slots[index] = new LinkedList<KeyValue<TKey, TValue>>();
            }

            foreach (var element in this.slots[index])
            {
                if (element.Key.Equals(key))
                {
                    throw new ArgumentException("Duplicate Key", key.ToString());
                    //throw new DuplicateKeyException("Duplicate Key", key.ToString());
                }
            }

            var newElement = new KeyValue<TKey, TValue>(key, value);
            this.slots[index].AddLast(newElement);
            this.Count++;
        }

        public bool AddOrReplace(TKey key, TValue value)
        {
            try
            {
                this.Add(key, value);
            }
            catch (ArgumentException argumentException)
            {
                if (argumentException.Message.Contains("Duplicate Key")
                    && argumentException.ParamName == key.ToString())
                {
                    int index = Math.Abs(key.GetHashCode()) % this.Capacity;
                    var keyValue = this.slots[index].FirstOrDefault(kvp => kvp.Key.Equals(key));
                    keyValue.Value = value;
                    return true;
                }
            }

            return false;
        }

        public TValue Get(TKey key)
        {
            var element = this.Find(key);

            if (element != null)
            {
                return element.Value;
            }

            throw new KeyNotFoundException();
        }

        public TValue this[TKey key]
        {
            get
            {
                return this.Get(key);
            }
            set
            {
                this.AddOrReplace(key, value);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var element = this.Find(key);

            if (element != null)
            {
                value = element.Value;
                return true;
            }

            value = default;
            return false;
        }

        public KeyValue<TKey, TValue> Find(TKey key)
        {
            int index = Math.Abs(key.GetHashCode()) % this.Capacity;

            if (this.slots[index] != null)
            {
                foreach (var element in this.slots[index])
                {
                    if (element.Key.Equals(key))
                    {
                        return element;
                    }
                }
            }

            return null;
        }

        public bool ContainsKey(TKey key)
        {
            return this.Find(key) != null;
        }

        public bool Remove(TKey key)
        {
            var element = this.Find(key);

            if (element != null)
            {
                int index = Math.Abs(key.GetHashCode()) % this.Capacity;

                this.slots[index].Remove(element);
                this.Count--;
                return true;
            }

            return false;
        }

        public void Clear()
        {
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[DefaultCapacity];
            this.Count = 0;
        }

        public IEnumerable<TKey> Keys => this.Select(kvp => kvp.Key);

        public IEnumerable<TValue> Values
        {
            get
            {
                var values = new List<TValue>();

                foreach (var slot in this.slots)
                {
                    if (slot != null)
                    {
                        foreach (var element in slot)
                        {
                            values.Add(element.Value);
                        }
                    }
                }

                return values;
            }
        }

        public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
        {
            foreach (var slot in this.slots)
            {
                if (slot != null)
                {
                    foreach (var element in slot)
                    {
                        yield return element;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private void GrowIfNeeded()
        {
            if ((float)(this.Count + 1) / this.Capacity >= LoadFactor)
            {
                var newTable = new HashTable<TKey, TValue>(this.Capacity * 2, this);

                this.slots = newTable.slots;
            }
        }
    }
}
