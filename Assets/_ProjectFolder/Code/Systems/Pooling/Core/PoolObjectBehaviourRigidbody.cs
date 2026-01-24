using UnityEngine;

namespace Unity.Pool
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PoolObjectBehaviourRigidbody : PoolObjectBehaviour
    {
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }

        protected virtual void Reset() => Rigidbody = GetComponent<Rigidbody2D>();
    }
}