using System.Collections.Generic;

namespace Unity.Pool
{
    public interface IPoolManagerObjects
    {
        PoolObjectBehaviour GetPrefab(ISplineResolution spline, string name);
        PoolObjectBehaviour GetPrefabRandom(ISplineResolution spline);
        PoolObjectBehaviour GetPrefabSequence(ISplineResolution spline);

        void RegisterGlobalDespawn(IPoolSpawner spawner);
        void UnregisterGlobalDespawn(IPoolSpawner spawner);

        void RegisterSpawn(ISplineResolution spline, IPoolSpawner action);
        void UnregisterSpawn(ISplineResolution spline, IPoolSpawner action);
        void RegisterDespawn(ISplineResolution spline, IPoolSpawner action);
        void UnregisterDespawn(ISplineResolution spline, IPoolSpawner action);
    }
    public interface IPoolSpawner
    {
        void OnCreate(PoolObjectBehaviour prefab);
        void OnRelease(PoolObjectBehaviour prefab);
    }

    public interface IPoolDisplaceObjects
    {
        IList<PoolObjectOnSpline> Spawned { get; }
        float SpeedMultiply { get; }
        float WorldOffset { get; set; }
    }
}