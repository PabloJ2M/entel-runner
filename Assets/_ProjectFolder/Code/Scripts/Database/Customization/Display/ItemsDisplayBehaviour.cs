using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.Customization
{
    using Services;
    using Pool;

    public abstract class ItemsDisplayBehaviour : PoolObjectSingle<ItemsDisplayEntry>
    {
        [Header("Controller")]
        [SerializeField] protected SO_LibraryReference_List _library;
        [SerializeField] protected SO_Item_List _itemList;

        protected CustomizationController _customization;

        protected IEnumerable<SO_Item> _items;
        protected Dictionary<string, Func<SO_Item, bool>> _filters = new();

        protected override void Awake()
        {
            base.Awake();
            _itemList?.Setup();
            _customization = UnityServiceInit.Instance?.GetComponentInChildren<CustomizationController>();
        }
        protected override void Reset() => _parent = GetComponentInChildren<ScrollRect>().content;
        protected override void OnGet(PoolObjectBehaviour @object)
        {
            base.OnGet(@object);
            @object.transform.SetAsLastSibling();
        }

        protected virtual void OnEnable()
        {
            _library.onItemTypeFiltered += OnFilteredItems;
            _library.onLibraryUpdated += OnUpdateLibrary;
            _library.onGroupUpdated += OnUpdateGroup;
        }
        protected virtual void OnDisable()
        {
            _library.onItemTypeFiltered -= OnFilteredItems;
            _library.onLibraryUpdated -= OnUpdateLibrary;
            _library.onGroupUpdated -= OnUpdateGroup;
        }

        protected virtual void OnUpdateLibrary(SO_LibraryReference reference) { }
        protected virtual void OnUpdateGroup(ItemGroup group) => DisplayItems();
        protected abstract void DisplayItems();

        protected void OnFilteredItems(ItemQuality type)
        {
            if (type == ItemQuality.None) _filters.Remove("type");
            else _filters["type"] = (item) => item.Type == type;
            DisplayItems();
        }
        protected IEnumerable<SO_Item> GetItemsFiltered()
        {
            var result = _items;
            foreach (var filter in _filters) result = _items.Where(filter.Value);
            return result;
        }

        public async void SaveData() => await _customization?.SaveData();
    }
}