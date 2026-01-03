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
        protected string _libraryID;

        protected override void Awake()
        {
            base.Awake();
            _customization = UnityServiceInit.Instance.GetComponentInChildren<CustomizationController>();
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

        protected virtual void OnUpdateLibrary(SO_LibraryReference reference) => _libraryID = reference.ID;
        protected abstract void OnUpdateCategory(string category);
        public async void SaveData() => await _customization.SaveData();
    }
}