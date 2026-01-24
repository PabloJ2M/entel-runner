using System.Collections.Generic;
using UnityEngine;

namespace Unity.Pool
{
    public abstract class PoolBehaviuour<T> : MonoBehaviour where T : PoolObjectBehaviour
    {
        [SerializeField] protected Transform _parent;

        public IList<T> Spawned { get; set; } = new List<T>();
        protected int LastIndex => Spawned.Count - 1;

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
        protected void ClearPoolInstance()
        {
            for (int i = Spawned.Count - 1; i >= 0; i--)
                Spawned[i].Destroy();
        }
    }
}