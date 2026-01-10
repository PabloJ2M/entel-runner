using UnityEngine;

namespace Unity.Achievements
{
    public class AchievementTrigger : MonoBehaviour
    {
        [SerializeField] private AchievementType _type;
        private int _amount;

        private void OnDisable()
        {
            AchievementEvent.onAction?.Invoke(_type, _amount);
            _amount = 0;
        }

        public void AddProgress(int amount) => _amount += amount;
    }
}