using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

namespace Unity.Pool
{
    public interface IPoolManagerObjects
    {
        float SpeedMultiply { get; }
        IList<PoolObjectOnSpline> Spawned { get; }

        event Action<PoolObjectBehaviour> OnSpawnObject;
        event Action<PoolObjectBehaviour> OnDespawnObject;
    }

    public abstract class PoolManagerObjects : PoolObjectMultiple<PoolObjectOnSpline>, IPoolManagerObjects
    {
        [SerializeField] protected int _capacity = 10;
        protected SplineContainer _spline;

        public virtual float SpeedMultiply { get; } = 1f;
        public event Action<PoolObjectBehaviour> OnSpawnObject;
        public event Action<PoolObjectBehaviour> OnDespawnObject;

        protected override void Awake()
        {
            base.Awake();
            _spline = GetComponentInChildren<SplineContainer>();
        }

        protected override PoolObjectOnSpline OnCreate(PoolObjectOnSpline prefab)
        {
            var clone = base.OnCreate(prefab);
            clone.Spline = _spline;
            return clone;
        }
        protected override void OnGet(PoolObjectBehaviour @object)
        {
            base.OnGet(@object);
            OnSpawnObject?.Invoke(@object);
        }
        protected override void OnRelease(PoolObjectBehaviour @object)
        {
            base.OnRelease(@object);
            OnDespawnObject?.Invoke(@object);
        }
    }
}