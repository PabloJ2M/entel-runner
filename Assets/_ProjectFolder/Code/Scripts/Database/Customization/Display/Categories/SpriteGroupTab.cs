using UnityEngine;

namespace Unity.Customization
{
    public class SpriteGroupTab : MonoBehaviour
    {
        [SerializeField] private ItemGroup _group;
        private SpriteGroup _manager;

        private void Awake() => _manager = GetComponentInParent<SpriteGroup>();

        public void OnClickHandler(bool value)
        {
            if (value)
                _manager.UpdateGroup(_group);
        }
    }
}