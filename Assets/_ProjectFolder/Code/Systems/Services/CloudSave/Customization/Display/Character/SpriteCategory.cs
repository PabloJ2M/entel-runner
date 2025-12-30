using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Unity.Customization
{
    public class SpriteCategory : MonoBehaviour
    {
        [SerializeField] private LibraryReferenceList _reference;
        private string[] _categories;

        private void OnEnable() => _reference.onLibraryUpdated += OnLibraryUpdated;
        private void OnDisable() => _reference.onLibraryUpdated -= OnLibraryUpdated;

        private async void OnLibraryUpdated(LibraryReference reference)
        {
            _categories = reference.Asset.GetCategoryNames().ToArray();
            await Task.Yield();
            UpdateCategory(0);
        }
        public void UpdateCategory(int index)
        {
            if (index < _categories.Length)
                _reference.UpdateCategory(_categories[index]);
        }
    }
}