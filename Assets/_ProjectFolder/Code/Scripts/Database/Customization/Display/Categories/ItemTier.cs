using UnityEngine;
using UnityEngine.UI;

namespace Unity.Customization
{
    public class ItemTier : MonoBehaviour
    {
        [SerializeField] private SO_LibraryReference_List _reference;
        [SerializeField] private ItemType _selected;
        private ToggleGroup _group;

        private void Awake() => _group = GetComponent<ToggleGroup>();
        private void OnUpdateView() => _reference?.FilteredByType(_selected);

        public void UpdateTier(ItemType type)
        {
            _selected = type;
            OnUpdateView();
        }
        public void CheckGroupStatus()
        {
            if (_group.AnyTogglesOn()) return;
            _selected = ItemType.None;
            OnUpdateView();
        }
    }
}