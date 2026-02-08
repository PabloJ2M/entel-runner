using System;
using System.Collections.Generic;

namespace Unity.Pool
{
    public class PoolObjectSpawner : PoolObjectMultiple<PoolObjectOnSpline>, IPoolManagerObjects
    {
        protected Dictionary<ISplineResolution, Action<PoolObjectBehaviour>> _onSpawn = new();
        protected Dictionary<ISplineResolution, Action<PoolObjectBehaviour>> _onDespawn = new();

        public event Action<PoolObjectBehaviour> onGlobalDespawnObject;
        public IDictionary<ISplineResolution, Action<PoolObjectBehaviour>> onSpawnObject => _onSpawn;
        public IDictionary<ISplineResolution, Action<PoolObjectBehaviour>> onDespawnObject => _onDespawn;

        public override PoolObjectBehaviour GetPrefab(ISplineResolution spline, string reference)
        {
            var prefab = base.GetPrefab(spline, reference);
            if (onSpawnObject.ContainsKey(spline)) onSpawnObject[spline]?.Invoke(prefab);
            return prefab;
        }
        protected override void OnRelease(PoolObjectBehaviour @object)
        {
            base.OnRelease(@object);
            onGlobalDespawnObject?.Invoke(@object);

            if (@object is PoolObjectOnSpline prefab)
                if (onDespawnObject.ContainsKey(prefab.Spline))
                    onDespawnObject[prefab.Spline]?.Invoke(@object);
        }
    }
}