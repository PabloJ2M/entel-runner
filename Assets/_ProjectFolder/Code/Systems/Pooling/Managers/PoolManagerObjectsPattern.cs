using UnityEngine;

namespace Unity.Pool
{
    public class PoolManagerObjectsPattern : MonoBehaviour
    {
        [SerializeField] private SO_SpawnPaternList _list;
        [SerializeField] private int _dificultyLevel = 1;
        [SerializeField] private float _dificultyProgression = 100;
        [SerializeField] private float _speedMultiply = 1f;
        [SerializeField] private float _distanceDelay = 5f;

        [SerializeField] private SpawnerPointByPattern[] _paths;

        private GameplayManager _gameManager;
        private SO_SpawnPatern _currentPattern;

        private float _startDistance, _dificultyDistance;
        private int _currentIndex;
        private bool _isSpawning;

        private void Awake() => _gameManager = GameplayManager.Instance;
        private void OnEnable() => _gameManager.onDinstanceTraveled += GameUpdate;
        private void OnDisable() => _gameManager.onDinstanceTraveled -= GameUpdate;

        private void Start()
        {
            foreach (var path in _paths)
                path.SetSpeed(_speedMultiply);
        }
        private void GameUpdate(float worldDistance)
        {
            if (!_isSpawning)
            {
                _currentPattern = _list.GetRandomPattern(_dificultyLevel);
                _startDistance = worldDistance * _speedMultiply;
                _currentIndex = 0;

                _isSpawning = true;
                return;
            }

            if (_dificultyLevel < _list.Length) HandleDificulty(worldDistance);
            float traveled = worldDistance * _speedMultiply - _startDistance;

            if (traveled >= _currentPattern.totalDistance + _distanceDelay)
            {
                _isSpawning = false;
                return;
            }
            if (_currentIndex < _currentPattern.sequence.Count)
            {
                SpawnInfo info = _currentPattern.sequence[_currentIndex];

                if (traveled >= info.distance)
                {
                    _paths[info.laneIndex].OnSpawn(info.poolObjectName, worldDistance);
                    _currentIndex++;
                }
            }
        }
        private void HandleDificulty(float distance)
        {
            if (distance < _dificultyDistance) return;

            _dificultyDistance = distance + _dificultyProgression;
            _dificultyLevel++;
        }
    }
}