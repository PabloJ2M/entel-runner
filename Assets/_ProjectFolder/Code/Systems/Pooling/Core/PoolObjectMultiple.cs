using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Unity.Pool
{
    public abstract class PoolObject<T> : PoolBehaviuour<T> where T : PoolObjectBehaviour
    {
        [SerializeField] protected T _prefab;

        protected ObjectPool<PoolObjectBehaviour> Pool;

        protected virtual void Awake() => Pool = new(() => OnCreate(_prefab), OnGet, OnRelease, OnDestroyObject);

        protected virtual T OnCreate(T prefab)
        {
            var obj = Instantiate(prefab, _parent);
            obj.PoolReference = Pool;
            return obj;
        }
    }

    public abstract class PoolObjectMultiple<T> : PoolBehaviuour<T> where T : PoolObjectBehaviour
    {
        [SerializeField] protected T[] _prefabs;

        protected Dictionary<string, ObjectPool<PoolObjectBehaviour>> Pools = new();
        protected int _index;

        protected virtual void Awake()
        {
            foreach (var prefab in _prefabs)
                Pools.Add(prefab.name, new(() => OnCreate(prefab), OnGet, OnRelease, OnDestroyObject));
        }
        protected virtual T OnCreate(T prefab)
        {
            var obj = Instantiate(prefab, _parent);
            obj.PoolReference = Pools[prefab.name];
            return obj;
        }

        protected PoolObjectBehaviour GetPrefab(string reference) => Pools[reference].Get();
        protected PoolObjectBehaviour GetPrefab(T reference) => Pools[reference.name].Get();
        protected PoolObjectBehaviour GetPrefabRandom()
        {
            int random = Random.Range(0, _prefabs.Length);
            return GetPrefab(_prefabs[random].name);
        }
        protected PoolObjectBehaviour GetPrefabSequence()
        {
            _index++;
            _index %= _prefabs.Length;
            return GetPrefab(_prefabs[_index].name);
        }
    }
}