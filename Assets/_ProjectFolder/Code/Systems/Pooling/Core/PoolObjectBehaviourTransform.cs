using UnityEngine;

namespace Unity.Pool
{
    public abstract class PoolObjectBehaviourTransform : PoolObjectBehaviour
    {
        [field: SerializeField] public Transform Transform { get; private set; }

        protected virtual void Reset() => Transform = transform;
    }
}