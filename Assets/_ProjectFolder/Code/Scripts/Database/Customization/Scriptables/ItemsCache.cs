using System;
using System.Collections.Generic;

namespace Unity.Customization
{
    [Serializable] public class ItemWrapper { public List<SO_Item> items = new(); }
    [Serializable] public class ItemsCache
    {
        public Dictionary<(string library, string group), List<SO_Item>> byLibrary;
        public Dictionary<string, List<SO_Item>> byGroup;
        public Dictionary<(string library, string group, string id), SO_Item> byPath;

        public bool IsBuildCache() => byLibrary != null && byGroup != null && byPath != null;
        public void BuildCache(Dictionary<SO_LibraryReference, ItemWrapper> items)
        {
            byLibrary = new(); byGroup = new(); byPath = new();

            foreach (var kv in items)
            {
                foreach (var item in kv.Value.items)
                {
                    if (item == null) continue;
                    
                    var libraryKey = (kv.Key.ID, item.Group);
                    if (!byLibrary.TryGetValue(libraryKey, out var library)) byLibrary[libraryKey] = new();
                    byLibrary[libraryKey].Add(item);

                    if (!byGroup.TryGetValue(item.Group, out var group)) byGroup[item.Group] = new();
                    byGroup[item.Group].Add(item);

                    byPath[(kv.Key.ID, item.Group, item.ID)] = item;
                }
            }
        }
    }
}