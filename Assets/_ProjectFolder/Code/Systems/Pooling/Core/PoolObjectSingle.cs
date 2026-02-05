using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Unity.Pool
{
    public abstract class PoolObjectSingle<T> : PoolBehaviuour<T> where T : PoolObjectBehaviour
    {
        [SerializeField] protected T _prefab;

        protected ObjectPool<PoolObjectBehaviour> Pool;
        protected IList<PoolObjectBehaviour> _spawned;

        protected virtual void Awake()
        {
            Pool = new(OnCreate, OnGet, OnRelease, OnDestroyObject);
            _spawned = new List<PoolObjectBehaviour>();
        }
        protected virtual T OnCreate()
        {
            var obj = Instantiate(_prefab, _parent);
            obj.PoolReference = Pool;
            _spawned.Add(obj);
            return obj;
        }
        protected override void OnRelease(PoolObjectBehaviour @object)
        {
            base.OnRelease(@object);
            _spawned.Remove(@object);
        }

        protected void ClearPoolInstance()
        {
            for (int i = _spawned.Count - 1; i >= 0; i--)
                _spawned[i].Destroy();
        }
    }
}