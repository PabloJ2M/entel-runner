using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu(fileName = "item", menuName = "inventory/item")]
public class SO_Item : ScriptableObject
{
    [SerializeField] private SpriteLibraryAsset _asset;
    [SerializeField] private Sprite _image;
}