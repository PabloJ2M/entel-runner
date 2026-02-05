using UnityEngine;

namespace Unity.Pool
{
    public class SpawnerPoint : PoolManagerObjectsByDistance
    {
        [SerializeField] private SO_PrefabReference _reference;
        [SerializeField] private bool _useRandom = true;

        protected override void OnSpawn()
        {
            string id = _useRandom ? _reference.GetRandom() : _reference.GetSequence();

            var obj = _manager.GetPrefab(_spline, id) as PoolObjectOnSpline;
            obj.SetDistance(_gameManager.WorldDistance * SpeedMultiply + WorldOffset);
        }
    }
}