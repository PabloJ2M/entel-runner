using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Unity.Customization
{
    [CreateAssetMenu(fileName = "list", menuName = "storage/list", order = 0)]
    public class SO_ItemList : ScriptableObject
    {
        [SerializeField] private SpriteLibraryAsset _library;
        [SerializeField] private SO_Item[] _items;

        private Dictionary<string, Dictionary<string, SO_Item>> _cache;
        private const string _default = "default";

        public static SO_ItemList Instance { get; private set; }
        public SpriteLibraryAsset Library => _library;

        private void OnEnable() => Instance = this;
        private void BuildCache()
        {
            _cache = new();
            foreach (var item in _items)
            {
                if (item == null) continue;

                if (!_cache.TryGetValue(item.Category, out var byId))
                {
                    byId = new Dictionary<string, SO_Item>();
                    _cache[item.Category] = byId;
                }

                byId[item.ID] = item;
            }
        }

        public SO_Item GetItemByID(string category, string id)
        {
            if (_cache == null) BuildCache();

            if (_cache.TryGetValue(category, out var byId))
                if (byId.TryGetValue(id, out var item))
                    return item;

            if (_cache.TryGetValue(category, out byId) && byId.TryGetValue(_default, out var fallback))
                return fallback;
            
            return null;
        }
        public IEnumerable<SO_Item> GetItems(IEnumerable<string> category)
        {
            if (_items == null || category == null) yield break;
            var categorySet = new HashSet<string>(category);

            foreach (var item in _items)
            {
                if (item == null) continue;

                if (categorySet.Contains(item.Category))
                    yield return item;
            }
        }
    }
}