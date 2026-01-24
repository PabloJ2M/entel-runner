using Unity.Achievements;
using UnityEngine;

public class UIScoreByDistance : UIScore
{
    [SerializeField] private float _distancePerPoint;
    [SerializeField] private AchievementTrigger _trigger;
    private float _traveled;

    private void OnEnable() => GameplayManager.Instance.onDinstanceTraveled += SetDistance;
    private void OnDisable() => GameplayManager.Instance.onDinstanceTraveled -= SetDistance;

    public void SetDistance(float worldDistance)
    {
        if (worldDistance - _traveled < _distancePerPoint) return;
        _traveled = worldDistance;
        _trigger?.AddProgress(1);
        Add(1);
    }
}