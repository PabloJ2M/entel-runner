using System;
using System.Collections.Generic;

namespace Unity.Pool
{
    public class PoolObjectSpawner : PoolObjectMultiple<PoolObjectOnSpline>, IPoolManagerObjects
    {
        public IDictionary<ISplineResolution, Action<PoolObjectBehaviour>> onSpawnObject { get; set; }
        public IDictionary<ISplineResolution, Action<PoolObjectBehaviour>> onDespawnObject { get; set; }

        public event Action<PoolObjectBehaviour> onGlobalDespawnObject;

        protected override void Awake()
        {
            base.Awake();
            onSpawnObject = new Dictionary<ISplineResolution, Action<PoolObjectBehaviour>>();
            onDespawnObject = new Dictionary<ISplineResolution, Action<PoolObjectBehaviour>>();
        }

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