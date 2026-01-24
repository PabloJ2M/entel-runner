using UnityEngine;

public class UIScoreByDistance : UIScore
{
    [SerializeField] private float _distancePerPoint;
    private float _traveled;

    private void OnEnable() => GameplayManager.Instance.onDinstanceTraveled += SetDistance;
    private void OnDisable() => GameplayManager.Instance.onDinstanceTraveled -= SetDistance;

    public void SetDistance(float worldDistance)
    {
        if (worldDistance - _traveled < _distancePerPoint) return;
        _traveled = worldDistance;

        Add(1);
    }
}