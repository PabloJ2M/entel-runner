using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Unity.Customization
{
    using Services;

    [RequireComponent(typeof(SpriteLibrary))]
    public class SpriteLibraryHandler : MonoBehaviour
    {
        [SerializeField] private SO_LibraryReference_List _reference;
        [SerializeField] private SO_Item_List _listReference;

        private Dictionary<ItemGroup, HashSet<SpriteResolverListener>> _resolvers = new();
        private CustomizationController _customization;
        private SpriteLibrary _library;
        private Animator _animator;

        private void Start() => _reference?.FindReference(ref _customization.Local.selectedLibrary);
        private void OnDestroy() => _resolvers.Clear();

        private void Awake()
        {
            _listReference?.Setup();
            _library = GetComponent<SpriteLibrary>();
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

        private void OnUpdatePreview(SO_LibraryReference library)
        {
            _library.spriteLibraryAsset = library.Asset;
            _animator.runtimeAnimatorController = library.Animator;

            _customization.Local.selectedLibrary = library.ID;
            _customization.Local.equipped.TryGetValue(library.ID, out var equipped);

            if (equipped == null) equipped = new();

            string[] groups = Enum.GetNames(typeof(ItemGroup));
            foreach (var group in groups)
            {
                equipped.TryGetValue(group, out var id);
                var item = _listReference.GetItemByPath(library.ID, group, id);
                if (item != null) SetLabel(group, item.LabelName);
            }
        }

        public void AddListener(ItemGroup group, SpriteResolverListener element)
        {
            if (_resolvers.TryGetValue(group, out var list)) list.Add(element);
            else _resolvers.Add(group, new() { element });
        }
        public void SetLabel(string group, string label)
        {
            if (!Enum.TryParse(typeof(ItemGroup), group, out var type)) return;
            if (!_resolvers.TryGetValue((ItemGroup)type, out var list)) return;

            foreach (var item in list)
                item.SetLabel(label);
        }
    }
}