using UnityEngine;

public abstract class GameplayListener : MonoBehaviour
{
    protected GameplayManager _gameManager;
    protected double _lastTravel;

    protected virtual void Awake() => _gameManager = GameplayManager.Instance;
    protected virtual void OnEnable() => _gameManager.onDinstanceTraveled += GameUpdate;
    protected virtual void OnDisable() => _gameManager.onDinstanceTraveled -= GameUpdate;

    protected abstract void GameUpdate(double traveled);
    protected double DeltaDistance(double value, float length)
    {
        double delta = value - _lastTravel;
        if (delta < 0) delta += length;
        return delta;
    }
}