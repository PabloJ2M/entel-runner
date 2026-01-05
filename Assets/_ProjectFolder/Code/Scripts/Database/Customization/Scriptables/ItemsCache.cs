using System;
using System.Collections.Generic;

namespace Unity.Customization
{
    [Serializable] public class ItemWrapper { public List<SO_Item> items = new(); }
    [Serializable] public class ItemsCache
    {
        public Dictionary<(string library, string category), List<SO_Item>> byLibrary;
        public Dictionary<string, List<SO_Item>> byCategory;
        public Dictionary<(string library, string category, string id), SO_Item> byPath;

        public bool IsBuildCache() => byLibrary != null && byCategory != null && byPath != null;
        public void BuildCache(Dictionary<SO_LibraryReference, ItemWrapper> items)
        {
            byLibrary = new(); byCategory = new(); byPath = new();

            foreach (var kv in items)
            {
                foreach (var item in kv.Value.items)
                {
                    if (item == null) continue;
                    
                    var libraryKey = (kv.Key.ID, item.Category);
                    if (!byLibrary.TryGetValue(libraryKey, out var library)) byLibrary[libraryKey] = new();
                    byLibrary[libraryKey].Add(item);

                    if (!byCategory.TryGetValue(item.Category, out var category)) byCategory[item.Category] = new();
                    byCategory[item.Category].Add(item);

                    byPath[(kv.Key.ID, item.Category, item.ID)] = item;
                }
            }
        }
    }
}