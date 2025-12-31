using UnityEngine;
using UnityEngine.UI;

namespace Unity.Customization
{
    using Services;
    using Services.CloudSave;
    using Pool;

    public abstract class ItemsDisplayBehaviour : PoolObjectSingle<ItemsDisplayEntry>
    {
        [Header("Controller")]
        [SerializeField] protected LibraryReferenceList _library;
        [SerializeField] protected SO_Item_Container _itemList;

        protected PlayerDataService _playerData;

        protected override void Awake()
        {
            base.Awake();
            _playerData = UnityServiceInit.Instance.GetComponent<PlayerDataService>();
        }
        protected override void Reset() => _parent = GetComponentInChildren<ScrollRect>().content;
        protected override void OnGet(PoolObjectBehaviour @object)
        {
            base.OnGet(@object);
            @object.Transform.SetAsLastSibling();
        }

        protected virtual void OnEnable()
        {
            _library.onLibraryUpdated += OnUpdateLibrary;
            _library.onCategoryUpdated += OnUpdateCategory;
        }
        protected virtual void OnDisable()
        {
            _library.onLibraryUpdated -= OnUpdateLibrary;
            _library.onCategoryUpdated -= OnUpdateCategory;
        }

        protected abstract void OnUpdateLibrary(LibraryReference reference);
        protected abstract void OnUpdateCategory(string category);
        public async void SaveData()
        {
            var cloud = new CustomizationDataCloud(_playerData.Customization);
            await _playerData.SaveObjectData(_playerData.DataID, cloud, _playerData.Customization);
        }
    }
}