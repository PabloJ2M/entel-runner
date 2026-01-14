using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.Customization
{
    public class SpriteGroup : EnumToggleGroup<ItemGroup>
    {
        [SerializeField] private SO_LibraryReference_List _reference;

        protected override void OnEnable()
        {
            base.OnEnable();
            _reference.onLibraryUpdated += OnLibraryUpdated;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            _reference.onLibraryUpdated -= OnLibraryUpdated;
        }

        private async void OnLibraryUpdated(SO_LibraryReference reference)
        {
            await Task.Yield();
            UpdateType(0);
        }
        public override void UpdateType(ItemGroup value)
        {
            if (value == _selected) return;

            _reference?.UpdateGroup(value);
            _selected = value;
        }
    }
}