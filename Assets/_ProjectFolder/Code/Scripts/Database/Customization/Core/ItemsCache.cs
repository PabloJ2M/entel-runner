using System;
using System.Collections.Generic;
using UnityEngine.Rendering;

namespace Unity.Customization
{
    [Serializable] public class ItemDictionary : SerializedDictionary<SO_LibraryReference, ItemWrapper> { }
    [Serializable] public class ItemWrapper { public List<SO_Item> items = new(); }
    [Serializable] public class ItemList : List<SO_Item> { }

    [Serializable] public class LibraryList : Dictionary<(string library, string group), ItemList> { }
    [Serializable] public class GroupList : Dictionary<string, ItemList> { }
    [Serializable] public class ItemPath : Dictionary<string, SO_Item> { }

    [Serializable] public struct ItemsCache
    {
        private LibraryList _libraryPath;
        private GroupList _groupPath;
        private ItemPath _itemPath;

        public ItemsCache(ItemDictionary dictionary)
        {
            _libraryPath = new(); _groupPath = new(); _itemPath = new();

            foreach (var libraryEntry in dictionary)
            {
                foreach (var item in libraryEntry.Value.items)
                {
                    if (item == null) continue;

                    var libraryKey = (libraryEntry.Key.ID, item.Group);
                    if (!_libraryPath.TryGetValue(libraryKey, out var library)) _libraryPath[libraryKey] = new();
                    _libraryPath[libraryKey].Add(item);

                    if (!_groupPath.TryGetValue(item.Group, out var group)) _groupPath[item.Group] = new();
                    _groupPath[item.Group].Add(item);

                    _itemPath[$"{libraryKey.ID}/{item.Group}/{item.ID}"] = item;
                }
            }
        }

        public IReadOnlyList<SO_Item> GetItemsByLibrary(string library, string group)
        {
            if (_libraryPath.TryGetValue((library, group), out var items)) return items;
            return Array.Empty<SO_Item>();
        }
        public IReadOnlyList<SO_Item> GetItemsByGroup(string group)
        {
            if (_groupPath.TryGetValue(group, out var items)) return items;
            return Array.Empty<SO_Item>();
        }

        public SO_Item GetItemByPath(string library, string group, string id)
        {
            _itemPath.TryGetValue($"{library}/{group}/{id}", out var item);
            return item;
        }
        public void ClearModifiedData()
        {
            foreach (var item in _itemPath.Values) {
                item.Discount = 0;
            }
        }
    }
}