using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Customization
{
    [CreateAssetMenu(fileName = "SpriteLibraryReferenceList", menuName = "system/customization/library reference list", order = 0)]
    public class SO_LibraryReference_List : ScriptableObject
    {
        [SerializeField] private SO_LibraryReference _default;
        [SerializeField] private List<SO_LibraryReference> _assets;
        private byte _index;

        public event Action<SO_LibraryReference> onLibraryUpdated;
        public event Action<SO_LibraryReference> onPreviewUpdated;
        public event Action<string> onCategoryUpdated;

        public void Previous()
        {
            _index--;
            if (_index >= _assets.Count) _index = (byte)(_assets.Count - 1);
            UpdateAssetReference();
        }
        public void Next()
        {
            _index++;
            _index %= (byte)_assets.Count;
            UpdateAssetReference();
        }

        private void UpdateAssetReference() { UpdatePreview(); UpdateLibrary(); }
        public void UpdatePreview() => onPreviewUpdated?.Invoke(_assets[_index]);
        public void UpdateLibrary() => onLibraryUpdated?.Invoke(_assets[_index]);
        public void UpdateCategory(string category) => onCategoryUpdated?.Invoke(category);

        public void FindReference(string key)
        {
            int index = _assets.FindIndex(x => x.ID == key);
            if (index < 0) index = _assets.FindIndex(x => x == _default);

            _index = (byte)index;
        }
        public void FindReference(ref string key)
        {
            FindReference(key);
            key = _assets[_index].ID;
            UpdateAssetReference();
        }
    }
}