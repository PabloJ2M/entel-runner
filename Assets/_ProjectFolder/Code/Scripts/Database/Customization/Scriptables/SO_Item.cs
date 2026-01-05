using UnityEngine;

namespace Unity.Customization
{
    using Services.Economy;

    [CreateAssetMenu(fileName = "item", menuName = "system/customization/item", order = 1)]
    public class SO_Item : ScriptableObject
    {
        [SerializeField] private SO_LibraryReference _libraryReference;
        [SerializeField] private string _itemID;
        [SerializeField] private string _category, _label;

        [SerializeField] private ItemType _type;
        [SerializeField] private BalanceType _balance;
        [SerializeField] private uint _cost;

        public SO_LibraryReference Reference => _libraryReference;
        public Sprite Sprite => _libraryReference?.Asset.GetSprite(Category, Label);

        public string Category => _category;
        public string Label => _label;
        public string ID => _itemID;

        public ItemType Type => _type;
        public BalanceType Balance => _balance;
        public uint Cost => !HasDiscount ? _cost : OverrideCost;
        public uint OverrideCost { get; set; }
        public bool HasDiscount => OverrideCost != 0;
    }
}