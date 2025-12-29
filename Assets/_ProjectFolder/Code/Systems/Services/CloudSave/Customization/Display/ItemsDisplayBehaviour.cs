using UnityEngine;
using UnityEngine.UI;

namespace Unity.Customization
{
    using Services.CloudSave;
    using Pool;

    public abstract class ItemsDisplayBehaviour : PoolObjectSingle<ItemsDisplayEntry>
    {
        [Header("Controller")]
        [SerializeField] protected LibraryReferenceList _library;
        [SerializeField] protected SO_ItemList _itemList;

        protected PlayerDataService _playerData;
        protected LibraryReference _selected;
        protected SpriteCategory _tabs;

        protected override void Awake()
        {
            base.Awake();
            _tabs = GetComponentInChildren<SpriteCategory>();
            _playerData = FindFirstObjectByType<PlayerDataService>(FindObjectsInactive.Include);
        }
        protected override void Reset() => _parent = GetComponentInChildren<ScrollRect>().content;
        protected override void OnGet(PoolObjectBehaviour @object)
        {
            base.OnGet(@object);
            @object.Transform.SetAsLastSibling();
        }

        private void OnEnable() => _library.onLibraryUpdated += OnUpdateLibrary;
        private void OnDisable() => _library.onLibraryUpdated -= OnUpdateLibrary;

        protected virtual void OnUpdateLibrary(LibraryReference reference)
        {
            _selected = reference;
            Clear();
        }

        public async void SaveData()
        {
            var cloud = new CustomizationDataCloud(_playerData.Customization);
            await _playerData.SaveObjectData(_playerData.DataID, cloud, _playerData.Customization);
        }
    }
}