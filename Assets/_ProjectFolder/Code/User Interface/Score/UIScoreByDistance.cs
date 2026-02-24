using UnityEngine;
using Unity.Achievements;
using Unity.Services;
using Unity.Services.Economy;

public class UIScoreByDistance : UIScore
{
    [Header("Score Properties")]
    [SerializeField] private GameplayManager _gameplayManager;
    [SerializeField] private AchievementTrigger _trigger;
    [SerializeField] private int _pointsPerCoin;
    [SerializeField] private float _distancePerPoint;
    
    private double _traveled;

    private void OnEnable() => _gameplayManager.onDinstanceTraveled += SetDistance;
    private void OnDisable() => _gameplayManager.onDinstanceTraveled -= SetDistance;

    public override void SaveNewScore()
    {
        base.SaveNewScore();

        var economy = UnityServiceInit.Instance?.GetComponent<PlayerEconomyService>();
        if (!economy) return;

        uint coins = (uint)(Score / _pointsPerCoin);
        if (coins != 0) economy.AddBalanceID(BalanceType.COIN, coins);
    }

    public void SetDistance(double worldDistance)
    {
        if (!_gameplayManager.IsEnabled || worldDistance - _traveled < _distancePerPoint) return;
        _traveled = worldDistance;
        _trigger?.AddProgress(1);
        Add(1);
    }
}