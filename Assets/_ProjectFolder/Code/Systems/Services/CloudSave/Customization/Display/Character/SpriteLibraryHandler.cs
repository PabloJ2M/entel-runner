using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Unity.Customization
{
    [RequireComponent(typeof(SpriteLibrary))]
    public class SpriteLibraryHandler : MonoBehaviour
    {
        [SerializeField] private LibraryReferenceList _list;
        private SpriteLibrary _library;

        private void Awake() => _library = GetComponent<SpriteLibrary>();
        private void OnEnable() => _list.onLibraryUpdated += SelectionHandler;
        private void OnDisable() => _list.onLibraryUpdated -= SelectionHandler;

        private void SelectionHandler(LibraryReference reference) => _library.spriteLibraryAsset = reference.Asset;
    }
}