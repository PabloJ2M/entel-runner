using UnityEngine;

namespace System.Collections.Generic
{
    [Serializable]
    public class SerializableHashSet<T> : ISerializationCallbackReceiver, IEnumerable<T>, IEquatable<ISet<T>>
    {
        [SerializeField] private List<T> list = new();

        private HashSet<T> _set;
        private HashSet<T> Set
        {
            get
            {
                if (_set == null) _set = new HashSet<T>(list);
                return _set;
            }
        }

        public SerializableHashSet() => list = new();
        public SerializableHashSet(HashSet<T> values)
        {
            list = new();
            foreach (T item in values) list.Add(item);
        }

        public int Count => Set.Count;

        public bool Add(T item)
        {
            if (Set.Add(item))
            {
                list.Add(item);
                return true;
            }
            return false;
        }
        public bool Remove(T item)
        {
            if (Set.Remove(item))
            {
                list.Remove(item);
                return true;
            }
            return false;
        }
        public void Clear()
        {
            Set.Clear();
            list.Clear();
        }

        public bool Contains(T item) => Set.Contains(item);

        public void OnBeforeSerialize()
        {
            list.Clear();
            list.AddRange(Set);
        }
        public void OnAfterDeserialize()
        {
            _set = new(list);
        }

        public IEnumerator<T> GetEnumerator() => Set.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool Equals(ISet<T> other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other == null) return false;
            return Set.SetEquals(other);
        }
        public override bool Equals(object obj) => Equals(obj as ISet<T>);
        public override int GetHashCode()
        {
            int hash = 0;
            foreach (var item in Set)
                hash ^= EqualityComparer<T>.Default.GetHashCode(item);

            return hash;
        }
    }
}