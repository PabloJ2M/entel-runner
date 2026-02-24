using System.Collections.Generic;
using UnityEngine;

namespace Unity.Pool
{
    public abstract class PoolManagerObjects : MonoBehaviour, IPoolSpawner, IPoolDisplaceObjects
    {
        [SerializeField] private PoolObjectSpawner _spawnHandler;
        protected IPoolManagerObjects _manager;
        protected ISplineResolution _spline;

        public IList<PoolObjectOnSpline> Spawned { get; protected set; }
        public virtual float SpeedMultiply { get; } = 1f;
        public virtual float WorldOffset { get; set; }

        protected virtual void Awake()
        {
            Spawned = new List<PoolObjectOnSpline>();
            _spline = GetComponentInChildren<ISplineResolution>();
            _manager = _spawnHandler;
        }
        protected virtual void OnEnable()
        {
            _manager.RegisterSpawn(_spline, this);
            _manager.RegisterDespawn(_spline, this);
        }
        protected virtual void OnDisable()
        {
            _manager.UnregisterSpawn(_spline, this);
            _manager.UnregisterDespawn(_spline, this);
        }

        void IPoolSpawner.OnCreate(PoolObjectBehaviour prefab) => OnCreate(prefab);
        void IPoolSpawner.OnRelease(PoolObjectBehaviour prefab) => OnRelease(prefab);

        protected void OnCreate(PoolObjectBehaviour prefab)
        {
            if (prefab is PoolObjectOnSpline @object)
                Spawned.Add(@object);
        }
        protected void OnRelease(PoolObjectBehaviour prefab)
        {
            if (prefab is PoolObjectOnSpline @object)
                Spawned.Remove(@object);
        }
        protected void Clear()
        {
            for (int i = Spawned.Count - 1; i >= 0; i--)
                Spawned[i].Destroy();
        }
    }
}