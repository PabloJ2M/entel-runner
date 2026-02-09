using System;
using System.Collections.Generic;

namespace Unity.Pool
{
    public interface IPoolManagerObjects
    {
        event Action<PoolObjectBehaviour> onGlobalDespawnObject;

        PoolObjectBehaviour GetPrefab(ISplineResolution spline, string name);
        PoolObjectBehaviour GetPrefabRandom(ISplineResolution spline);
        PoolObjectBehaviour GetPrefabSequence(ISplineResolution spline);

        void RegisterSpawn(ISplineResolution spline, Action<PoolObjectBehaviour> action);
        void UnregisterSpawn(ISplineResolution spline, Action<PoolObjectBehaviour> action);
        void RegisterDespawn(ISplineResolution spline, Action<PoolObjectBehaviour> action);
        void UnregisterDespawn(ISplineResolution spline, Action<PoolObjectBehaviour> action);
    }
    public interface IPoolDisplaceObjects
    {
        IList<PoolObjectOnSpline> Spawned { get; }
        float SpeedMultiply { get; }
        float WorldOffset { get; set; }
    }
}