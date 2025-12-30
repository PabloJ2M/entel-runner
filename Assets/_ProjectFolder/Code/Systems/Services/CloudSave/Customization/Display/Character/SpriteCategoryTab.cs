using UnityEngine;

namespace Unity.Customization
{
    public class SpriteCategoryTab : MonoBehaviour
    {
        private SpriteCategory _manager;
        private int _siblingIndex;

        private void Awake() => _manager = GetComponentInParent<SpriteCategory>();
        private void Start() => _siblingIndex = transform.GetSiblingIndex();

        public void OnClickHandler(bool value)
        {
            if (!value) return;
            _manager.UpdateCategory(_siblingIndex);
        }
    }
}