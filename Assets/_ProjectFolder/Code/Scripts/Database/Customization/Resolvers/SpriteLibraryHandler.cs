using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Unity.Customization
{
    using Services;

    public class SpriteLibraryHandler : MonoBehaviour
    {
        [SerializeField] private SO_LibraryReference_List _reference;
        [SerializeField] private SO_Item_List _listReference;
        [SerializeField] private UnlockedCharacter[] _objectReferences;

        private Dictionary<ItemGroup, HashSet<SpriteResolverListener>> _resolvers = new();
        private CustomizationController _customization;
        private SpriteLibrary _library;
        private Animator _animator;

        private void Start() => _reference?.FindReference(ref _customization.Local.selectedLibrary);
        private void OnDestroy() => _resolvers.Clear();

        private void Awake()
        {
            _listReference?.Setup();
            _library = GetComponentInChildren<SpriteLibrary>();
            _animator = GetComponentInParent<Animator>();
            _customization = UnityServiceInit.Instance?.GetComponentInChildren<CustomizationController>();
        }
        private void OnEnable()
        {
            _customization.onCustomizationUpdated += Start;
            _reference.onPreviewUpdated += OnUpdatePreview;
        }
        private void OnDisable()
        {
            _customization.onCustomizationUpdated -= Start;
            _reference.onPreviewUpdated -= OnUpdatePreview;
        }
        public void AddListener(ItemGroup group, SpriteResolverListener element)
        {
            if (_resolvers.TryGetValue(group, out var list)) list.Add(element);
            else _resolvers.Add(group, new() { element });
        }

        private void OnUpdatePreview(SO_LibraryReference library)
        {
            _library.spriteLibraryAsset = library.Asset;
            _animator.runtimeAnimatorController = library.Animator;

            _customization.Local.selectedLibrary = library.ID;
            _customization.Local.equipped.TryGetValue(library.ID, out var equipped);

            SetCharacters(library.ObjectReference);
            if (library.Asset) EquippeLibrary(library.ID, ref equipped);
        }
        private void SetCharacters(string objectName)
        {
            foreach (var item in _objectReferences)
            {
                bool enable = item.name == objectName;
                item.gameObject.SetActive(enable);

                if (enable) item.HasPurchased(true);
            }
        }
        private void EquippeLibrary(string libraryID, ref SerializedStringDictionary equipped)
        {
            if (equipped == null) equipped = new();
            ItemGroup[] groups = Enum.GetValues(typeof(ItemGroup)) as ItemGroup[];

            foreach (var group in groups) {
                string groupID = group.ToString();
                equipped.TryGetValue(groupID, out var id);
                var item = _listReference.GetItemByPath(libraryID, groupID, id);
                
                if (item == null) continue;
                SetLabel(group, item);
            }
        }
        private void SetLabel(ItemGroup group, SO_Item item)
        {
            if (!_resolvers.TryGetValue(group, out var resolvers)) return;

            foreach (var resolver in resolvers)
            {
                resolver.SetLabel(item.LabelName);
                resolver.SetSortingOrder(item.SortingGroup);
            }
        }
    }
}