using UnityEngine;

namespace Unity.Pool
{
    public class SpawnerPointByPattern : PoolManagerObjects
    {
        [SerializeField] private SO_SpawnPatern _pattern;
        [SerializeField] private int _minCapacityPerObject;

        private float _seedMultiply = 1f;
        public override float SpeedMultiply => _seedMultiply;

        private void Start()
        {
            foreach (var item in _pattern.sequence) {
                for (int i = 0; i < _minCapacityPerObject; i++)
                    OnSpawn(item.poolObjectName, 0, true);
            }

            Clear();
        }

        public void SetSpeed(float value) => _seedMultiply = value;
        public void OnSpawn(string item, float worldDistance, bool isAlloc = false)
        {
            var obj = _manager.GetPrefab(_spline, item) as PoolObjectOnSpline;
            obj.SetDistance(worldDistance * SpeedMultiply + WorldOffset);
            obj.IsAlloc = isAlloc;
        }
    }
}