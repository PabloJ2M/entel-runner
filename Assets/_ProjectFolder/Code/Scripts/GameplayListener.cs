using UnityEngine;

public abstract class GameplayListener : MonoBehaviour
{
    protected GameplayManager _gameManager;

    protected virtual void Awake() => _gameManager = GameplayManager.Instance;
    protected virtual void OnEnable() => _gameManager.onDinstanceTraveled += GameUpdate;
    protected virtual void OnDisable() => _gameManager.onDinstanceTraveled -= GameUpdate;

    protected abstract void GameUpdate(float traveled);
}