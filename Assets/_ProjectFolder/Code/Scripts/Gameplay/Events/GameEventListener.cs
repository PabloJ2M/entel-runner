using UnityEngine;

namespace Gameplay.Events
{
    public class GameEventListener : GameplayListener
    {
        [SerializeField] private float _spawnDistance = 900f;

        private IGameEvent _handler;
        private double _traveled;

        protected override void Awake()
        {
            base.Awake();
            _handler = GetComponentInChildren<IGameEvent>();
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            _gameManager.onGameStarted.AddListener(StartDistance);
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            _gameManager.onGameStarted.RemoveListener(StartDistance);
        }

        private void StartDistance() => _traveled = _gameManager.WorldDistance;

        protected override void GameUpdate(double traveled)
        {
            if (!_gameManager.IsEnabled) return;
            if (traveled - _traveled < _spawnDistance) return;

            StartEvent();
        }

        public void StartEvent()
        {
            _gameManager.Pause();
            _handler.OnStartEvent();
        }
        public async void CompleteEvent(bool success)
        {
            _traveled = _gameManager.WorldDistance;
            _handler.OnCompleteEvent(success);

            await Awaitable.WaitForSecondsAsync(1);

            _gameManager.Play();
        }
    }
}