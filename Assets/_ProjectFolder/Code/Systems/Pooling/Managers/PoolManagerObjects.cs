using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Pool
{
    public interface IPoolManagerObjects
    {
        IDictionary<ISplineResolution, Action<PoolObjectBehaviour>> onSpawnObject { get; }
        IDictionary<ISplineResolution, Action<PoolObjectBehaviour>> onDespawnObject { get; }

        event Action<PoolObjectBehaviour> onGlobalDespawnObject;

        PoolObjectBehaviour GetPrefab(ISplineResolution spline, string name);
        PoolObjectBehaviour GetPrefabRandom(ISplineResolution spline);
        PoolObjectBehaviour GetPrefabSequence(ISplineResolution spline);
    }
    public interface IPoolDisplaceObjects
    {
        IList<PoolObjectOnSpline> Spawned { get; }
        float SpeedMultiply { get; }
        float WorldOffset { get; set; }
    }

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
            if (_manager.onSpawnObject.ContainsKey(_spline)) _manager.onSpawnObject[_spline] += OnCreate;
            else _manager.onSpawnObject[_spline] = OnCreate;

            if (_manager.onDespawnObject.ContainsKey(_spline)) _manager.onDespawnObject[_spline] += OnRelease;
            else _manager.onDespawnObject[_spline] = OnRelease;
        }
        protected virtual void OnDisable()
        {
            if (_manager.onSpawnObject.ContainsKey(_spline))
                _manager.onSpawnObject[_spline] -= OnCreate;

            if (_manager.onDespawnObject.ContainsKey(_spline))
                _manager.onDespawnObject[_spline] -= OnRelease;
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