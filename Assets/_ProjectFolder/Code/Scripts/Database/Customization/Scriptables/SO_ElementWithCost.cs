using UnityEngine;

namespace Unity.Customization
{
    using Services.Economy;

    public abstract class SO_ElementWithCost : ScriptableObject
    {
        [SerializeField] private BalanceType _balance;
        [SerializeField] private uint _cost;

        public BalanceType Balance => _balance;
        public uint Cost => !HasDiscount ? _cost : (uint)(_cost * Discount);

        public float Discount { get; set; }
        public bool HasDiscount => Discount != 0;
    }
}