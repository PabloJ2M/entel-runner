//using System;
using UnityEngine;

namespace Unity.Customization
{
    using Services.Economy;

    [CreateAssetMenu(fileName = "item", menuName = "system/customization/item", order = 1)]
    public class SO_Item : ScriptableObject
    {
        [SerializeField] private SO_LibraryReference _libraryReference;
        [SerializeField] private string _itemID;
        [SerializeField] private Sprite _preview;

        [SerializeField] private ItemGroup _group;
        //[SerializeField] private string[] _categories;
        [SerializeField] private string _label;

        [SerializeField] private ItemQuality _quality;
        [SerializeField] private BalanceType _balance;
        [SerializeField] private uint _cost;

        public string ID => _itemID;
        public SO_LibraryReference Reference => _libraryReference;

        //public Sprite PreviewImage {
        //    get {
        //        if (!_preview) _preview = Preview.Build(Reference.GetSprites(_categories, _label));
        //        return _preview;
        //    }
        //}

        public Sprite Preview => _preview;
        public string Group => _group.ToString();
        public string LabelName => _label;

        public ItemQuality Type => _quality;
        public BalanceType Balance => _balance;
        public uint Cost => !HasDiscount ? _cost : OverrideCost;
        public uint OverrideCost { get; set; }
        public bool HasDiscount => OverrideCost != 0;
    }
}