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

        private Dictionary<string, SpriteResolverListener> _resolvers = new();
        private CustomizationController _customization;
        private SpriteLibrary _library;

        private void Start() => _reference?.FindReference(ref _customization.Local.selectedLibrary);
        private void OnDestroy() => _resolvers.Clear();

        private void Awake()
        {
            _library = GetComponent<SpriteLibrary>();
            _customization = UnityServiceInit.Instance.GetComponentInChildren<CustomizationController>();
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
            _customization.Local.selectedLibrary = library.ID;
            _customization.Local.equipped.TryGetValue(library.ID, out var equipped);

            if (equipped == null) equipped = new();

            IEnumerable<string> categories = library.Asset.GetCategoryNames();
            foreach (var category in categories)
            {
                equipped.TryGetValue(category, out var id);
                var item = _listReference.GetItemByID(library.ID, category, id);
                if (item != null) SetLabel(category, item.Label);
            }
        }

        public void AddListener(string category, SpriteResolverListener element) => _resolvers.Add(category, element);
        public void RemoveListener(string category) => _resolvers.Remove(category);
        public void SetLabel(string category, string label)
        {
            if (_resolvers.TryGetValue(category, out var resolver))
                resolver.SetLabel(label);
        }
    }
}