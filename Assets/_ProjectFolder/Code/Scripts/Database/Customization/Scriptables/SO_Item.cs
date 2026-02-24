using System.Collections.Generic;
using UnityEngine;

namespace Unity.Customization
{
    [CreateAssetMenu(fileName = "item", menuName = "system/customization/item", order = 1)]
    public class SO_Item : SO_ElementWithCost
    {
        [SerializeField] private SO_LibraryReference _libraryReference;
        [SerializeField] private string _itemID;
        [SerializeField] private Sprite _preview;
        [SerializeField] private string _displayName;

        [SerializeField] private SO_SortingGroup _sorting;
        [SerializeField] private ItemGroup _group;
        [SerializeField] private string _label;

        [SerializeField] private ItemQuality _quality;

        public string ID => _itemID;
        public ItemQuality Type => _quality;
        public SO_LibraryReference Reference => _libraryReference;

        public Sprite Preview => _preview;
        public string DisplayName => _displayName;
        public string Group => _group.ToString();
        public IReadOnlyList<Sort> SortingGroup => _sorting.Sorts;
        public string LabelName => _label;
    }
}