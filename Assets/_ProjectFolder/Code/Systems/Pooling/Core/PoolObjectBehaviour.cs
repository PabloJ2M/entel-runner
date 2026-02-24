using UnityEngine;
using UnityEngine.Pool;

namespace Unity.Pool
{
    public class PoolObjectBehaviour : MonoBehaviour
    {
        public IObjectPool<PoolObjectBehaviour> PoolReference { protected get; set; }
        public bool IsAlloc { get; set; }

        public virtual void Enable() => gameObject.SetActive(true);
        public virtual void Disable() => gameObject.SetActive(false);
        public virtual void Destroy() => PoolReference?.Release(this);
    }
}