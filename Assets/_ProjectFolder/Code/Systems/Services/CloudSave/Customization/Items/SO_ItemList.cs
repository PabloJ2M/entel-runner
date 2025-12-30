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

        private Dictionary<string, Dictionary<string, Dictionary<string, SO_Item>>> _cache;
        private const string _default = "default";

        private void BuildCache()
        {
            _cache = new();

            foreach (var list in _items)
            {
                if (!_cache.ContainsKey(list.Key.ID)) _cache.Add(list.Key.ID, new());

                foreach (var item in list.Value.items)
                {
                    if (!_cache[list.Key.ID].ContainsKey(item.Category)) _cache[list.Key.ID].Add(item.Category, new());
                    _cache[list.Key.ID][item.Category][item.ID] = item;
                }
            }
        }

        public IEnumerable<SO_Item> GetItems(string category)
        {
            if (_items == null || string.IsNullOrEmpty(category)) yield break;

            foreach (var library in _items)
            {
                foreach (var item in library.Value.items)
                {
                    if (item == null) continue;
                    if (item.Category == category) yield return item;
                }
            }
        }
        public IDictionary<string, Dictionary<string, SO_Item>> GetItemsLibrary(string libraryID)
        {
            if (_cache.TryGetValue(libraryID, out var data)) return data;
            else return null;
        }

        public SO_Item GetItemByID(string library, string category, string id)
        {
            if (_cache == null) BuildCache();

            GetItem(library, category, id, out var item);
            if (item != null) return item;

            GetItem(library, category, _default, out item);
            return item;
        }
        private void GetItem(string library, string category, string id, out SO_Item item)
        {
            if (_cache.TryGetValue(library, out var byCategory)) {
                if (byCategory.TryGetValue(category, out var byId)) {
                    byId.TryGetValue(id, out item);
                    return;
                }
            }

            item = null;
        }
    }
}