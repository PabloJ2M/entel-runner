using UnityEngine;

namespace Unity.Achievements
{
    public class AchievementTrigger : MonoBehaviour
    {
        [SerializeField] private AchievementType _type;

        public void AddProgress(int amount) => AchievementEvent.onAction?.Invoke(_type, amount);
    }
}