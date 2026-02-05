using System.Collections;
using UnityEngine;
using Unity.Pool;

public class BossManager : GameplayListener
{
    [SerializeField] private PoolManagerObjectsPattern _patternManager;
    [SerializeField] private float _spawnDelay = 900f;

    private float _traveled;
    private bool _isEnabled;

    protected override void GameUpdate(float traveled)
    {
        if (_isEnabled) return;
        if (traveled - _traveled < _spawnDelay) return;

        SetStatus(true);
        StartCoroutine(BossLoop());
    }

    private IEnumerator BossLoop()
    {
        yield return new WaitForSeconds(20);
        SetStatus(false);
    }
    private void SetStatus(bool value)
    {
        _isEnabled = value;
        _patternManager.IsEnabled = !value;
    }
}