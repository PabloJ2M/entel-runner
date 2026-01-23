using UnityEngine;

namespace Unity.Pool
{
    [RequireComponent(typeof(IPoolManagerObjects))]
    public sealed class PoolObjectDisplacement : MonoBehaviour
    {
        [SerializeField] private DisplacementType _type = DisplacementType.Distance;
        [SerializeField] private bool _useGlobalSpeed = true;

        private enum DisplacementType { Time, Distance };

        private GameManager _gameManager;
        private IPoolManagerObjects _manager;

        private void Awake()
        {
            _gameManager = GameManager.Instance;
            _manager = GetComponent<IPoolManagerObjects>();
        }
        private void Update()
        {
            if (!_useGlobalSpeed) return;

            switch (_type)
            {
                case DisplacementType.Time: MoveTime(_gameManager.Speed * 0.1f); break;
                case DisplacementType.Distance: Translate(_gameManager.Speed); break;
            }
        }

        public void Translate(float speed) => MoveDistance(speed, Time.deltaTime);
        public void TranslateUnit() => MoveDistance(1f);
        public void TranslateUnitBackwards() => MoveDistance(-1f);

        public void MoveDistance(float speed, float delta = 1f)
        {
            if (speed == 0) return;
            float d = speed * _manager.SpeedMultiply * delta;

            for (int i = _manager.Spawned.Count - 1; i >= 0; i--)
                _manager.Spawned[i].AddDistance(d);
        }
        public void MoveTime(float speed)
        {
            if (speed == 0) return;
            float t = speed * _manager.SpeedMultiply * Time.deltaTime;

            for (int i = _manager.Spawned.Count - 1; i >= 0; i--)
                _manager.Spawned[i].AddTime(t);
        }
    }
}