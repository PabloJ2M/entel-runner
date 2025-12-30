using System.Collections.Generic;
using UnityEngine;

namespace Unity.Customization
{
    using Services.CloudSave;

    [RequireComponent(typeof(SpriteLibraryHandler))]
    public class SpriteResolverController : MonoBehaviour
    {
        [SerializeField] private LibraryReferenceList _reference;
        [SerializeField] private SO_Item_Container _listReference;

        private Dictionary<string, SpriteResolverElement> _resolvers = new();
        private SpriteLibraryHandler _library;
        private PlayerDataService _player;

        private void Start() => _reference?.FindReference(ref _player.Customization.selectedLibrary);
        private void OnDestroy() => _resolvers.Clear();

        private void Awake()
        {
            _library = GetComponent<SpriteLibraryHandler>();
            _player = FindFirstObjectByType<PlayerDataService>(FindObjectsInactive.Include);
        }
        private void OnEnable()
        {
            _player.onDataUpdated += Start;
            _reference.onPreviewUpdated += OnUpdatePreview;
        }
        private void OnDisable()
        {
            _player.onDataUpdated -= Start;
            _reference.onPreviewUpdated -= OnUpdatePreview;
        }

        private void OnUpdatePreview(LibraryReference library)
        {
            _library.SelectionHandler(library);

            _player.Customization.selectedLibrary = library.ID;
            _player.Customization.equipped.TryGetValue(library.ID, out var equipped);
            if (equipped == null) equipped = new();

            IEnumerable<string> categories = library.Asset.GetCategoryNames();
            foreach (var category in categories)
            {
                equipped.TryGetValue(category, out var id);
                var item = _listReference.GetItemByID(library.ID, category, id);
                if (item != null) SetLabel(category, item.Label);
            }
        }

        public void AddListener(string category, SpriteResolverElement element) => _resolvers.Add(category, element);
        public void RemoveListener(string category) => _resolvers.Remove(category);
        public void SetLabel(string category, string label)
        {
            if (_resolvers.TryGetValue(category, out var resolver))
                resolver.SetLabel(label);
        }
    }
}