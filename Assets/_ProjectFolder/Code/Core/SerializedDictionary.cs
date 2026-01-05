using System;
using UnityEngine.Rendering;

[Serializable] public class StringHashSet : SerializedHashSet<string> { }
[Serializable] public class StringDictionary : SerializedDictionary<string, string> { }
[Serializable] public class HashSetDictionary : SerializedDictionary<string, StringHashSet> { }
[Serializable] public class NestedStringDictionary : SerializedDictionary<string, StringDictionary> { }
[Serializable] public class NestedHashSetDictionary : SerializedDictionary<string, HashSetDictionary> { }

public static class SerializedExtensions
{
    public static bool ExistPath(this NestedStringDictionary reference, string library, string category, string id)
    {
        if (!reference.ContainsKey(library)) reference.Add(library, new());
        if (!reference[library].TryGetValue(category, out var item)) return false;
        return item == id;
    }
    public static bool ExistPath(this NestedHashSetDictionary reference, string library, string category, string id)
    {
        if (!reference.ContainsKey(library)) reference.Add(library, new());
        if (!reference[library].TryGetValue(category, out var items)) return false;
        return items.Contains(id);
    }

    public static void CreatePath(this NestedStringDictionary reference, string library, string category, string id)
    {
        if (!reference.ContainsKey(library)) reference.Add(library, new());
        if (!reference[library].ContainsKey(category)) reference[library].Add(category, id);
        else reference[library][category] = id;
    }
    public static void CreatePath(this NestedHashSetDictionary reference, string library, string category, string id)
    {
        if (!reference.ContainsKey(library)) reference.Add(library, new());
        if (!reference[library].ContainsKey(category)) reference[library].Add(category, new() { id });
        else reference[library][category].Add(id);
    }
}