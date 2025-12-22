using System.Collections.Generic;
using UnityEngine;

namespace Unity.Pool
{
    public abstract class PoolBehaviuour<T> : MonoBehaviour where T : PoolObjectBehaviour
    {
        [SerializeField] protected Transform _parent;

        protected int LastIndex => Spawned.Count - 1;

        public IList<T> Spawned { get; set; } = new List<T>();

        protected virtual void Reset() => _parent = transform;

        protected virtual void OnGet(PoolObjectBehaviour @object)
        {
            @object.Enable();
            Spawned?.Add(@object as T);
        }
        protected virtual void OnRelease(PoolObjectBehaviour @object)
        {
            @object.Disable();
            Spawned?.Remove(@object as T);
        }
        protected virtual void OnDestroyObject(PoolObjectBehaviour @object) => Destroy(@object.gameObject);
        protected void Clear()
        {

        }
    }
}