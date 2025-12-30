using System.Collections.Generic;
using UnityEngine;

namespace Unity.Customization
{
    using Services.CloudSave;

    [RequireComponent(typeof(SpriteLibraryHandler))]
    public class SpriteResolverController : MonoBehaviour
    {
        [SerializeField] private LibraryReferenceList _reference;
        [SerializeField] private SO_ItemList _listReference;

        private Dictionary<string, SpriteResolverElement> _resolvers = new();
        private PlayerDataService _player;

        private void Awake() => _player = FindFirstObjectByType<PlayerDataService>(FindObjectsInactive.Include);
        private void Start() => _reference?.FindReference(ref _player.Customization.selectedLibrary);
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
            var data = _player.Customization;
            data.selectedLibrary = library.ID;

            if (!data.equipped.ContainsKey(library.ID)) data.equipped.Add(library.ID, new());

            foreach (var category in data.equipped[library.ID])
            {
                var item = _listReference.GetItemByID(library.ID, category.Key, category.Value);
                if (item == null) continue;

                SetLabel(category.Key, item.Label);
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