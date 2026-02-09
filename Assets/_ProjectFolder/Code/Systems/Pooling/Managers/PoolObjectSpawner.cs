using System;
using System.Collections.Generic;

namespace Unity.Pool
{
    public class PoolObjectSpawner : PoolObjectMultiple<PoolObjectOnSpline>, IPoolManagerObjects
    {
        protected Dictionary<ISplineResolution, Action<PoolObjectBehaviour>> _onSpawn = new();
        protected Dictionary<ISplineResolution, Action<PoolObjectBehaviour>> _onDespawn = new();

        public event Action<PoolObjectBehaviour> onGlobalDespawnObject;

        public override PoolObjectBehaviour GetPrefab(ISplineResolution spline, string reference)
        {
            var prefab = base.GetPrefab(spline, reference);
            if (_onSpawn.ContainsKey(spline)) _onSpawn[spline]?.Invoke(prefab);
            return prefab;
        }
        protected override void OnRelease(PoolObjectBehaviour @object)
        {
            base.OnRelease(@object);
            onGlobalDespawnObject?.Invoke(@object);

            if (@object is PoolObjectOnSpline prefab)
                if (_onDespawn.ContainsKey(prefab.Spline))
                    _onDespawn[prefab.Spline]?.Invoke(@object);
        }

        public void RegisterSpawn(ISplineResolution spline, Action<PoolObjectBehaviour> action)
        {
            if (!_onSpawn.ContainsKey(spline)) _onSpawn.Add(spline, null);
            _onSpawn[spline] += action;
        }
        public void UnregisterSpawn(ISplineResolution spline, Action<PoolObjectBehaviour> action)
        {
            if (!_onSpawn.ContainsKey(spline)) return;
            _onSpawn[spline] -= action;
        }
        public void RegisterDespawn(ISplineResolution spline, Action<PoolObjectBehaviour> action)
        {
            if (!_onDespawn.ContainsKey(spline)) _onDespawn.Add(spline, null);
            _onDespawn[spline] += action;
        }
        public void UnregisterDespawn(ISplineResolution spline, Action<PoolObjectBehaviour> action)
        {
            if (!_onDespawn.ContainsKey(spline)) return;
            _onDespawn[spline] -= action;
        }
    }
}