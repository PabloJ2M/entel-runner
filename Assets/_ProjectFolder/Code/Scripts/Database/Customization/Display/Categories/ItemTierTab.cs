using UnityEngine;

namespace Unity.Customization
{
    public class ItemTierTab : MonoBehaviour
    {
        [SerializeField] private ItemType _type;
        private ItemTier _manager;

        private void Awake() => _manager = GetComponentInParent<ItemTier>();
        public void OnClickHandler(bool value)
        {
            if (value) _manager.UpdateTier(_type);
            else _manager.CheckGroupStatus();
        }
    }
}