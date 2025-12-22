using UnityEngine;
using UnityEngine.Pool;

namespace Unity.Pool
{
    public abstract class PoolObjectSingle<T> : PoolBehaviuour<T> where T : PoolObjectBehaviour
    {
        [SerializeField] protected T _prefab;

        protected ObjectPool<PoolObjectBehaviour> Pool;

        protected virtual void Awake() => Pool = new(OnCreate, OnGet, OnRelease, OnDestroyObject);
        protected virtual T OnCreate()
        {
            var obj = Instantiate(_prefab, _parent);
            obj.PoolReference = Pool;
            return obj;
        }
    }
}