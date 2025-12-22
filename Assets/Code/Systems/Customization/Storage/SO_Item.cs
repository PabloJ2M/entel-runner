using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Unity.Customization
{
    [CreateAssetMenu(fileName = "item", menuName = "inventory/item")]
    public class SO_Item : ScriptableObject
    {
        [SerializeField] private SpriteLibraryAsset _assetReference;
        [SerializeField] private string _category, _label;
        [SerializeField] private uint _price;

        public Sprite Sprite => _assetReference.GetSprite(Category, Label);
        public string Category => _category;
        public string Label => _label;
        public uint Price => _price;
    }
}