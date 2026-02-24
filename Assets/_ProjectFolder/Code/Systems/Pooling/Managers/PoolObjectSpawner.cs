using System.Collections.Generic;

namespace Unity.Pool
{
    public class PoolObjectSpawner : PoolObjectMultiple<PoolObjectOnSpline>, IPoolManagerObjects
    {
        protected Dictionary<ISplineResolution, IPoolSpawner> _onSpawn = new();
        protected Dictionary<ISplineResolution, IPoolSpawner> _onDespawn = new();

        protected List<IPoolSpawner> globalDespawnObject = new();

        public override PoolObjectBehaviour GetPrefab(ISplineResolution spline, string reference)
        {
            var prefab = base.GetPrefab(spline, reference);
            if (_onSpawn.ContainsKey(spline)) _onSpawn[spline]?.OnCreate(prefab);
            return prefab;
        }
        protected override void OnRelease(PoolObjectBehaviour @object)
        {
            base.OnRelease(@object);

            if (!@object.IsAlloc)
            {
                foreach (var spawner in globalDespawnObject)
                    spawner.OnRelease(@object);
            }

            if (@object is not PoolObjectOnSpline prefab) return;

            if (_onDespawn.ContainsKey(prefab.Spline))
                _onDespawn[prefab.Spline]?.OnRelease(@object);
        }

        public void RegisterGlobalDespawn(IPoolSpawner spawner) => globalDespawnObject.Add(spawner);
        public void UnregisterGlobalDespawn(IPoolSpawner spawner) => globalDespawnObject.Remove(spawner);

        public void RegisterSpawn(ISplineResolution spline, IPoolSpawner spawner) => _onSpawn[spline] = spawner;
        public void UnregisterSpawn(ISplineResolution spline, IPoolSpawner spawner) => _onSpawn[spline] = null;
        public void RegisterDespawn(ISplineResolution spline, IPoolSpawner spawner) => _onDespawn[spline] = spawner;
        public void UnregisterDespawn(ISplineResolution spline, IPoolSpawner spawner) => _onDespawn[spline] = null;
    }
}