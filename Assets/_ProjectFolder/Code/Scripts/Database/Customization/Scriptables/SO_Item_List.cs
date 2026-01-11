using System.Collections.Generic;
using UnityEngine;

namespace Unity.Customization
{
    using Services.CloudSave;

    [CreateAssetMenu(fileName = "_container", menuName = "system/customization/item container", order = 0)]
    public class SO_Item_List : ScriptableObject, ICloudSaveGameData
    {
        [SerializeField] private ItemDictionary _items;

        private const string _default = "default";
        private ItemsCache? _cache;

        public void Setup()
        {
            if (_cache == null)
                _cache = new(_items);
        }
        public string ItemsListToJson()
        {
            IJsonData data = new ItemsCloud(_items);
            return data.JsonDictionary();
        }

        public IReadOnlyList<SO_Item> GetItemsByLibrary(string libraryID, string groupID) =>
            _cache?.GetItemsByLibrary(libraryID, groupID);
        public IReadOnlyList<SO_Item> GetItemsByGroup(string groupID) =>
            _cache?.GetItemsByGroup(groupID);

        public SO_Item GetItemByPath(string library, string group, string id) =>
            _cache?.GetItemByPath(library, group, string.IsNullOrEmpty(id) ? _default : id);

        public void ClearModifiedData() => _cache?.ClearModifiedData();
    }
}