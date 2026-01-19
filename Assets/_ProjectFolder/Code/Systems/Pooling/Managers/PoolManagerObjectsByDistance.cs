using UnityEngine;

namespace Unity.Pool
{
    public abstract class PoolManagerObjectsByDistance : PoolManagerObjects
    {
        [Header("Distance")]
        [SerializeField] private float _spaceDistance = 1f;
        [SerializeField] private float _speedMultiply = 1f;
        [SerializeField] private bool _buildOnAwake;

        private GameManager _gameManager;
        private float _traveled;

        public override float SpeedMultiply => _speedMultiply;

        protected override void Awake()
        {
            base.Awake();
            _gameManager = GameManager.Instance;
        }
        protected virtual void Start()
        {
            if (!_buildOnAwake) return;

            for (int i = 0; i < _capacity; i++)
            {
                OnSpawn();
                Spawned[i].SetDistance(_spaceDistance * i);
            }
        }

        protected virtual void Update() => ForceDistance(_gameManager.Speed * SpeedMultiply);
        protected abstract void OnSpawn();
        
        public void ForceDistance(float amount) => Math.Loop(ref _traveled, amount * Time.deltaTime, _spaceDistance, OnSpawn);
    }
}