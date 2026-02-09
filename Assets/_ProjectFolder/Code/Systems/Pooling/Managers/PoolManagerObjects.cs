using System.Collections.Generic;
using UnityEngine;

namespace Unity.Pool
{
    public abstract class PoolManagerObjects : MonoBehaviour, IPoolDisplaceObjects
    {
        protected IPoolManagerObjects _manager;
        protected ISplineResolution _spline;

        public IList<PoolObjectOnSpline> Spawned { get; protected set; }
        public virtual float SpeedMultiply { get; } = 1f;
        public virtual float WorldOffset { get; set; }

        protected virtual void Awake()
        {
            Spawned = new List<PoolObjectOnSpline>();
            _manager = FindFirstObjectByType<PoolObjectSpawner>();
            _spline = GetComponentInChildren<ISplineResolution>();
        }
        protected virtual void OnEnable()
        {
            _manager.RegisterSpawn(_spline, OnCreate);
            _manager.RegisterDespawn(_spline, OnRelease);
        }
        protected virtual void OnDisable()
        {
            _manager.UnregisterSpawn(_spline, OnCreate);
            _manager.UnregisterDespawn(_spline, OnRelease);
        }

        protected void OnCreate(PoolObjectBehaviour prefab)
        {
            if (prefab is not PoolObjectOnSpline @object) return;
            Spawned.Add(@object);
        }
        protected void OnRelease(PoolObjectBehaviour prefab)
        {
            if (prefab is not PoolObjectOnSpline @object) return;
            Spawned.Remove(@object);
        }
    }
}