using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Unity.Customization
{
    using Services.CloudSave;

    [CreateAssetMenu(fileName = "_container", menuName = "system/customization/item container", order = 0)]
    public class SO_Item_List : ScriptableObject, ICloudSaveGameData
    {
        [SerializeField] private SerializedDictionary<SO_LibraryReference, ItemWrapper> _items;

        private const string _default = "default";
        private ItemsCache _cache = new();

        public string ItemsListToJson()
        {
            return string.Empty;
        }

        public IReadOnlyList<SO_Item> GetItemsByLibrary(string libraryID, string categoryID)
        {
            if (!_cache.IsBuildCache()) _cache.BuildCache(_items);
            if (_cache.byLibrary.TryGetValue((libraryID, categoryID), out var items)) return items;
            return Array.Empty<SO_Item>();
        }
        public IReadOnlyList<SO_Item> GetItemsByCategory(string categoryID)
        {
            if (!_cache.IsBuildCache()) _cache.BuildCache(_items);
            if (_cache.byCategory.TryGetValue(categoryID, out var items)) return items;
            return Array.Empty<SO_Item>();
        }
        public SO_Item GetItemByID(string library, string category, string id)
        {
            if (!_cache.IsBuildCache()) _cache.BuildCache(_items);
            _cache.byPath.TryGetValue((library, category, string.IsNullOrEmpty(id) ? _default : id), out var item);
            return item;
        }
    }
}