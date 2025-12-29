using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Unity.Customization
{
    [Serializable]
    public class ItemWrapper
    {
        public List<SO_Item> items = new();
    }

    [CreateAssetMenu(fileName = "list", menuName = "customization/list", order = 0)]
    public class SO_ItemList : ScriptableObject
    {
        [SerializeField] private SerializedDictionary<LibraryReference, ItemWrapper> _items;
        private Dictionary<LibraryReference, Dictionary<string, Dictionary<string, SO_Item>>> _cache;
        private const string _default = "default";

        private void BuildCache()
        {
            _cache = new();

            foreach (var list in _items)
            {
                if (!_cache.ContainsKey(list.Key)) _cache.Add(list.Key, new());

                foreach (var item in list.Value.items)
                {
                    if (!_cache[list.Key].ContainsKey(item.Category)) _cache[list.Key].Add(item.Category, new());
                    _cache[list.Key][item.Category][item.ID] = item;
                }
            }
        }

        public IEnumerable<SO_Item> GetItems(LibraryReference library, string category)
        {
            if (_items == null || string.IsNullOrEmpty(category)) yield break;

            foreach (var item in _items[library].items)
            {
                if (item == null) continue;
                if (category == item.Category) yield return item;
            }
        }
        public IEnumerable<SO_Item> GetItems(LibraryReference library, string category, ISet<string> ids)
        {
            if (_items == null || string.IsNullOrEmpty(category)) yield break;

            foreach (var item in _items[library].items)
            {
                if (item == null) continue;
                if (category == item.Category && (item.Cost == 0 || ids.Contains(item.ID))) yield return item;
            }
        }

        public SO_Item GetItemByID(LibraryReference library, string category, string id)
        {
            if (_cache == null) BuildCache();

            GetItem(library, category, id, out var item);
            if (item != null) return item;

            GetItem(library, category, _default, out item);
            return item;
        }
        private void GetItem(LibraryReference library, string category, string id, out SO_Item item)
        {
            if (_cache.TryGetValue(library, out var byCategory))
                if (byCategory.TryGetValue(category, out var byId)) {
                    byId.TryGetValue(id, out item);
                    return;
                }

            item = null;
        }
    }
}