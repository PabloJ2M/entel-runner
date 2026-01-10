using UnityEngine;

namespace Unity.Achievements
{
    [RequireComponent(typeof(AchievementController))]
    public class AchievementTracker : MonoBehaviour
    {
        private AchievementController _controller;

        private void Awake() => _controller = GetComponent<AchievementController>();
        private void OnEnable() => AchievementEvent.onAction += OnActionHandler;
        private void OnDisable() => AchievementEvent.onAction -= OnActionHandler;
        private void OnDestroy() => AchievementEvent.onAction -= OnActionHandler;

        private void OnActionHandler(AchievementType type, int amount)
        {
            foreach (var achievements in _controller.Achievements)
            {
                foreach (var achievement in achievements.Value) {
                    if (achievement.Status.isCompleted) continue;
                    if (achievement.Type == type) achievement.Add(amount);
                }
            }
        }
    }
}