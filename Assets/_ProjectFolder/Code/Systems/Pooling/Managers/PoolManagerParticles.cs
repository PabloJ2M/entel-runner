using UnityEngine;

namespace Unity.Pool
{
    [RequireComponent(typeof(IPoolManagerObjects))]
    public abstract class PoolManagerParticles : PoolObject<PoolParticle>
    {
        protected IPoolManagerObjects _spawner;

        protected override void Awake()
        {
            base.Awake();
            _spawner = GetComponent<IPoolManagerObjects>();
        }
        protected virtual void OnEnable() => _spawner.onGlobalDespawnObject += OnReleaseObjectEffect;
        protected virtual void OnDisable() => _spawner.onGlobalDespawnObject -= OnReleaseObjectEffect;

        protected abstract void OnReleaseObjectEffect(PoolObjectBehaviour @object);
    }
}