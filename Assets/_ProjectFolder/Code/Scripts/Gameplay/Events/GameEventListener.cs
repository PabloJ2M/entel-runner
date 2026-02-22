using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Events
{
    public class GameEventListener : GameplayListener
    {
        [SerializeField] private float _firstSpawnDistance = 150, _spawnDistance = 300f;
        [SerializeField] private UnityEvent<bool> _onStatusEventChanged;

        private IGameEvent _handler;
        private float _spawnLength;
        private double _traveled;

        protected override void Awake()
        {
            base.Awake();
            _handler = GetComponentInChildren<IGameEvent>();
            _onStatusEventChanged.Invoke(false);
            _spawnLength = _firstSpawnDistance;
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            _gameManager.onGameStarted.AddListener(StartDistance);
            _gameManager.onGameCompleted.AddListener(StopEvent);
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            _gameManager.onGameStarted.RemoveListener(StartDistance);
            _gameManager.onGameCompleted.RemoveListener(StopEvent);
        }

        private void StartDistance() => _traveled = _gameManager.WorldDistance;
        private void StopEvent()
        {
            _traveled = _gameManager.WorldDistance;
            _handler.OnCompleteEvent(false);
        }

        protected override void GameUpdate(double traveled)
        {
            if (!_gameManager.IsEnabled) return;
            if (traveled - _traveled < _spawnLength) return;

            _spawnLength = _spawnDistance;
            StartEvent();
        }

        public void StartEvent()
        {
            _onStatusEventChanged.Invoke(true);
            _gameManager.Pause();
            _handler.OnStartEvent();
        }
        public async void CompleteEvent(bool success)
        {
            _traveled = _gameManager.WorldDistance;
            _handler.OnCompleteEvent(success);

            await Awaitable.WaitForSecondsAsync(1);

            _gameManager.Play();
            _onStatusEventChanged.Invoke(false);
        }
    }
}