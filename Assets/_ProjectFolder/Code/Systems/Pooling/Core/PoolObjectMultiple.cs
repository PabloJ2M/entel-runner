using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Unity.Pool
{
    public abstract class PoolObjectMultiple<T> : PoolBehaviuour<T> where T : PoolObjectBehaviour
    {
        [SerializeField] protected T[] _prefabs;

        protected Dictionary<T, ObjectPool<PoolObjectBehaviour>> Pools = new();

        protected virtual void Awake()
        {
            foreach (var prefab in _prefabs)
                Pools.Add(prefab, new(() => OnCreate(prefab), OnGet, OnRelease, OnDestroyObject));
        }
        protected virtual T OnCreate(T prefab)
        {
            var obj = Instantiate(prefab, _parent);
            obj.PoolReference = Pools[prefab];
            return obj;
        }

        protected PoolObjectBehaviour GetPrefabRandom()
        {
            var prefab = _prefabs[Random.Range(0, _prefabs.Length)];
            return Pools[prefab].Get();
        }
    }
}