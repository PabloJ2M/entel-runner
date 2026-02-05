using UnityEngine;

namespace Unity.Pool
{
    public abstract class PoolBehaviuour<T> : MonoBehaviour where T : PoolObjectBehaviour
    {
        [SerializeField] protected Transform _parent;

        protected virtual void Reset() => _parent = transform;

        protected virtual void OnGet(PoolObjectBehaviour @object) => @object.Enable();
        protected virtual void OnRelease(PoolObjectBehaviour @object) => @object.Disable();
        protected virtual void OnDestroyObject(PoolObjectBehaviour @object) => Destroy(@object.gameObject);
    }
}