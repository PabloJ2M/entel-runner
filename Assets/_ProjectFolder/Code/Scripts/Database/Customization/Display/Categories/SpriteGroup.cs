using System.Threading.Tasks;
using UnityEngine;

namespace Unity.Customization
{
    public class SpriteGroup : MonoBehaviour
    {
        [SerializeField] private SO_LibraryReference_List _reference;

        private void OnEnable() => _reference.onLibraryUpdated += OnLibraryUpdated;
        private void OnDisable() => _reference.onLibraryUpdated -= OnLibraryUpdated;

        public void UpdateGroup(ItemGroup group) => _reference.UpdateGroup(group);

        private async void OnLibraryUpdated(SO_LibraryReference reference)
        {
            await Task.Yield();
            UpdateGroup(0);
        }
    }
}