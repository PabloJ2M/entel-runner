using UnityEngine;

namespace Unity.Customization
{
    [CreateAssetMenu(fileName = "item", menuName = "customization/item", order = 1)]
    public class SO_Item : ScriptableObject
    {
        [SerializeField] private LibraryReference _libraryReference;
        [SerializeField] private string _itemID;
        [SerializeField] private string _category, _label;
        [SerializeField] private uint _cost;

        public LibraryReference Reference => _libraryReference;
        public Sprite Sprite => _libraryReference?.Asset.GetSprite(Category, Label);

        public string Category => _category;
        public string Label => _label;
        public string ID => _itemID;
        public uint Cost => _cost;
    }
}