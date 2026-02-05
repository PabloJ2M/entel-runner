using UnityEngine;

namespace Unity.Pool
{
    public abstract class PoolManagerObjectsByDistance : PoolManagerObjects
    {
        [Header("Spawn")]
        [SerializeField] private int _startLength;
        [SerializeField] private float _spaceDistance = 1f;
        [SerializeField] private float _speedMultiply = 1f;
        [SerializeField] private bool _buildOnAwake;

        protected GameplayManager _gameManager;
        private float _lastSpawn;

        public override float SpeedMultiply => _speedMultiply;

        protected virtual void Start()
        {
            if (!_buildOnAwake) return;
            _lastSpawn = -_spaceDistance;

            for (int i = 0; i < _startLength; i++) {
                OnSpawn();
                WorldOffset += _spaceDistance;
            }
        }
        protected override void Awake()
        {
            base.Awake();
            _gameManager = GameplayManager.Instance;
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            _gameManager.onDinstanceTraveled += GameUpdate;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            _gameManager.onDinstanceTraveled -= GameUpdate;
        }
        protected abstract void OnSpawn();

        private void GameUpdate(float worldDistance)
        {
            float laneDistance = worldDistance * _speedMultiply;

            if (laneDistance - _lastSpawn < _spaceDistance) return;
            _lastSpawn = laneDistance;
            OnSpawn();
        }
    }
}