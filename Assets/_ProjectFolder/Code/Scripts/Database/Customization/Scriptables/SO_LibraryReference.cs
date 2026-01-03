using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Unity.Customization
{
    [CreateAssetMenu(fileName = "SpriteLibraryReference", menuName = "system/customization/library reference")]
    public class SO_LibraryReference : ScriptableObject
    {
        [SerializeField] private string _libraryID;
        [SerializeField] private SpriteLibraryAsset _assetReference;

        public string ID => _libraryID;
        public SpriteLibraryAsset Asset => _assetReference;
    }
}