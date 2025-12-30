using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Unity.Customization
{
    [RequireComponent(typeof(SpriteLibrary))]
    public class SpriteLibraryHandler : MonoBehaviour
    {
        private SpriteLibrary _library;

        private void Awake() => _library = GetComponent<SpriteLibrary>();

        public void SelectionHandler(LibraryReference reference) => _library.spriteLibraryAsset = reference.Asset;
    }
}