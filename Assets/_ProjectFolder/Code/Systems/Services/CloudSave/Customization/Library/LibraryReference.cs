using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Unity.Customization
{
    [CreateAssetMenu(fileName = "SpriteLibraryReference", menuName = "customization/library reference")]
    public class LibraryReference : ScriptableObject
    {
        [SerializeField] private string _libraryID;
        [SerializeField] private SpriteLibraryAsset _assetReference;

        public string ID => _libraryID;
        public SpriteLibraryAsset Asset => _assetReference;
    }
}