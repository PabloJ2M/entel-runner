using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializedHashSet<T> : IEnumerable<T>, ISet<T>, IEquatable<ISet<T>>, ISerializationCallbackReceiver
{
    [SerializeField] private List<T> _list = new();

    private HashSet<T> _hashSet;
    public HashSet<T> HashSet
    {
        get {
            if (_hashSet == null) _hashSet = new(_list);
            return _hashSet;
        }
    }

    public SerializedHashSet() { }
    public SerializedHashSet(IEnumerable<T> values)
    {
        foreach (var item in values)
            Add(item);
    }

    public int Count => HashSet.Count;
    public bool IsReadOnly => false;

    public bool Add(T item)
    {
        if (!HashSet.Add(item)) return false;
        _list.Add(item);
        return true;
    }
    public bool Remove(T item)
    {
        if (!HashSet.Remove(item)) return false;
        _list.Remove(item);
        return true;
    }
    public void Clear() { HashSet.Clear(); _list.Clear(); }

    void ICollection<T>.Add(T item) => Add(item);

    public bool Contains(T item) => HashSet.Contains(item);
    public void CopyTo(T[] array, int index) => HashSet.CopyTo(array, index);

    public void ExceptWith(IEnumerable<T> other) => HashSet.ExceptWith(other);
    public void IntersectWith(IEnumerable<T> other) => HashSet.IntersectWith(other);
    public bool IsProperSubsetOf(IEnumerable<T> other) => HashSet.IsProperSubsetOf(other);
    public bool IsProperSupersetOf(IEnumerable<T> other) => HashSet.IsProperSupersetOf(other);
    public bool IsSubsetOf(IEnumerable<T> other) => HashSet.IsSubsetOf(other);
    public bool IsSupersetOf(IEnumerable<T> other) => HashSet.IsSupersetOf(other);
    public bool Overlaps(IEnumerable<T> other) => HashSet.Overlaps(other);
    public bool SetEquals(IEnumerable<T> other) => HashSet.SetEquals(other);
    public void SymmetricExceptWith(IEnumerable<T> other) => HashSet.SymmetricExceptWith(other);
    public void UnionWith(IEnumerable<T> other) => HashSet.UnionWith(other);

    public IEnumerator<T> GetEnumerator() => HashSet.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void OnBeforeSerialize() { _list.Clear(); _list.AddRange(HashSet); }
    public void OnAfterDeserialize() => _hashSet = new HashSet<T>(_list);
    public override int GetHashCode()
    {
        int hash = 17;
        foreach (var item in HashSet)
            hash = hash * 31 + item.GetHashCode();
        return hash;
    }

    public override bool Equals(object obj) => obj is ISet<T> other && HashSet.SetEquals(other);
    public bool Equals(ISet<T> other)
    {
        if (ReferenceEquals(this, other)) return true;
        if (other == null) return false;
        return HashSet.SetEquals(other);
    }
}