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
            foreach (var mission in _controller.DailyAchievements)
            {
                if (mission.Status.isCompleted) continue;
                if (mission.Type == type) mission.Add(amount);
            }
        }
    }
}