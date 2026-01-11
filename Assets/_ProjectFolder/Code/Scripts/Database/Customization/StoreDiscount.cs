using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Unity.Customization.Store
{
    using Services.RemoteConfig;

    public class StoreDiscount : RemoteConfigListener
    {
        protected override string _localDataID => "store_discounts";

        [SerializeField] private SO_LibraryReference_List _mainReference;
        [SerializeField] private SO_Item_List _itemList;
        [SerializeField] private ConfigType _configType;
        [SerializeField, Range(0f, 1f)] private float _discountPercent;

        [SerializeField] private ItemsRemote _items;

        protected override void Awake()
        {
            base.Awake();
            _itemList?.Setup();
        }
        private void Start()
        {
            LoadLocalData(ref _items);
            if (_items.discounts.Count != 0)
                OnRemoteConfigCompleted();
        }

        protected override void OnRemoteConfigUpdated(string key, DateTime serverTime)
        {
            if (!Enum.TryParse(key, out ConfigType type)) return;
            if (_configType != type) return;

            _items = JsonConvert.DeserializeObject<ItemsRemote>(_remoteConfig.GetJson(key));
            _itemList?.ClearModifiedData();
            SaveLocalData(_items);
        }
        protected override void OnRemoteConfigCompleted()
        {
            ParseConfigData();
            _mainReference?.UpdateGroup(ItemGroup.Head);
        }
        protected override void ParseConfigData()
        {
            foreach (var library in _items.discounts) {
                foreach (var group in library.Value) {
                    foreach (var itemId in group.Value)
                        ApplyDiscount(library.Key, group.Key, itemId);
                }
            }

            void ApplyDiscount(string library, string group, string id)
            {
                var item = _itemList?.GetItemByPath(library, group, id);
                if (item == null) return;

                item.Discount = _discountPercent;
            }
        }
    }
}