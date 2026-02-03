using UnityEngine;

namespace Unity.Pool
{
    public class SpawnerPoint : PoolManagerObjectsByDistance
    {
        [SerializeField] private bool _useRandom = true;

        protected override void OnSpawn()
        {
            var obj = (_useRandom ? GetPrefabRandom() : GetPrefabSequence()) as PoolObjectOnSpline;
            obj.SetDistance(_gameManager.WorldDistance * SpeedMultiply + WorldOffset);
        }
    }
}