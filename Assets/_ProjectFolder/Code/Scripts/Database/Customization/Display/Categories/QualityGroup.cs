using UnityEngine;
using UnityEngine.UI;

namespace Unity.Customization
{
    public class QualityGroup : EnumToggleGroup<ItemQuality>
    {
        [SerializeField] private SO_LibraryReference_List _reference;

        public override void UpdateType(ItemQuality value)
        {
            if (_selected == value) return;

            _reference?.FilteredByType(value);
            _selected = value;
        }
        public override void CheckToggleStatus()
        {
            if (AnyTogglesOn()) return;

            _selected = ItemQuality.None;
            _reference?.FilteredByType(_selected);
        }
    }
}