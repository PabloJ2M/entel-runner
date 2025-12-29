using System.Linq;
using UnityEngine;

namespace Unity.Customization
{
    public class SpriteCategory : MonoBehaviour
    {
        [SerializeField] private LibraryReferenceList _reference;
        private string[] _categories;

        public string Category { get; private set; }

        private void Awake() => OnLibraryUpdated(_reference.Default);

        private void OnLibraryUpdated(LibraryReference reference)
        {
            _categories = reference.Asset.GetCategoryNames().ToArray();
            if (!_categories.Contains(Category)) Category = _categories[0];
        }

        public void SetCategory(int index)
        {
            Category = _categories[index];
            _reference.UpdateLibraryReference();
        }
    }
}