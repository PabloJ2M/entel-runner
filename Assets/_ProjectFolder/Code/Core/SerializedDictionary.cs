using System;
using System.Collections.Generic;
using UnityEngine.Rendering;

public static class SerializedExtensions
{
    public static bool ExistPath(this SerializedNestedString reference, string library, string category, string id)
    {
        if (!reference.ContainsKey(library)) reference.Add(library, new());
        if (!reference[library].TryGetValue(category, out var item)) return false;
        return item == id;
    }
    public static bool ExistPath(this SerializedNestedHashSet reference, string library, string category, string id)
    {
        if (!reference.ContainsKey(library)) reference.Add(library, new());
        if (!reference[library].TryGetValue(category, out var items)) return false;
        return items.Contains(id);
    }

    public static void CreatePath(this SerializedNestedString reference, string library, string category, string id)
    {
        if (!reference.ContainsKey(library)) reference.Add(library, new());
        if (!reference[library].ContainsKey(category)) reference[library].Add(category, id);
        else reference[library][category] = id;
    }
    public static void CreatePath(this SerializedNestedHashSet reference, string library, string category, string id)
    {
        if (!reference.ContainsKey(library)) reference.Add(library, new());
        if (!reference[library].ContainsKey(category)) reference[library].Add(category, new() { id });
        else reference[library][category].Add(id);
    }
}

[Serializable] public class SerializedStringHashSet : SerializedHashSet<string> { }
[Serializable] public class StringHashSet : HashSet<string> { }
[Serializable] public class SerializedStringDictionary : SerializedDictionary<string, string> { }
[Serializable] public class SerializedHashSetDictionary : SerializedDictionary<string, SerializedStringHashSet> { }
[Serializable] public class HashSetDictionary : Dictionary<string, StringHashSet> { }
[Serializable] public class SerializedNestedString : SerializedDictionary<string, SerializedStringDictionary> { }
[Serializable] public class SerializedNestedHashSet : SerializedDictionary<string, SerializedHashSetDictionary> { }
[Serializable] public class NestedHashSet : Dictionary<string, HashSetDictionary> { }