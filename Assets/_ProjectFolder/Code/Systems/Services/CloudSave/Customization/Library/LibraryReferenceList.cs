using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Customization
{
    [CreateAssetMenu(fileName = "SpriteLibraryReferenceList", menuName = "customization/library reference list", order = 0)]
    public class LibraryReferenceList : ScriptableObject
    {
        [SerializeField] private LibraryReference _default;
        [SerializeField] private List<LibraryReference> _assets;
        private byte _index;

        public LibraryReference Default => _default;
        public event Action<LibraryReference> onLibraryUpdated;

        public void Previous()
        {
            _index--;
            if (_index >= _assets.Count) _index = (byte)(_assets.Count - 1);
            UpdateLibraryReference();
        }
        public void Next()
        {
            _index++;
            _index %= (byte)_assets.Count;
            UpdateLibraryReference();
        }

        public async void UpdateLibraryReference()
        {
            await Task.Yield();
            onLibraryUpdated?.Invoke(_assets[_index]);
        }

        public void FindReference(string key)
        {
            _index = (byte)Mathf.Clamp(_assets.FindIndex(x => x.ID == key), 0, _assets.Count);
        }
        public void FindReference(ref string key)
        {
            FindReference(key);
            key = _assets[_index].ID;
            UpdateLibraryReference();
        }
    }
}