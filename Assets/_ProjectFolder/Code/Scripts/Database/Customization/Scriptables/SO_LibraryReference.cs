using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Unity.Customization
{
    [CreateAssetMenu(fileName = "SpriteLibraryReference", menuName = "system/customization/library reference")]
    public class SO_LibraryReference : ScriptableObject
    {
        [SerializeField] private string _libraryID;

        [SerializeField] private SpriteLibraryAsset _assetReference;
        [SerializeField] private string _objectReference;

        [SerializeField] private RuntimeAnimatorController _animator;

        public string ID => _libraryID;
        public string ObjectReference => _objectReference;
        public SpriteLibraryAsset Asset => _assetReference;
        public RuntimeAnimatorController Animator => _animator;
    }
}