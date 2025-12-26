using UnityEngine;

namespace Unity.Customization
{
    [CreateAssetMenu(fileName = "item", menuName = "storage/item", order = 1)]
    public class SO_Item : ScriptableObject
    {
        [SerializeField] private string _itemID;
        [SerializeField] private string _category, _label;
        [SerializeField] private uint _cost;

        public Sprite Sprite => SO_ItemList.Instance.Library.GetSprite(Category, Label);
        public string ID => _itemID;
        public string Category => _category;
        public string Label => _label;
        public uint Cost => _cost;
    }
}