using UnityEngine;

namespace Unity.Pool
{
    [RequireComponent(typeof(IPoolManagerObjects))]
    public abstract class PoolManagerParticles : PoolObject<PoolParticle>, IPoolSpawner
    {
        protected IPoolManagerObjects _spawner;

        protected override void Awake()
        {
            base.Awake();
            _spawner = GetComponent<IPoolManagerObjects>();
        }
        protected virtual void OnEnable() => _spawner.RegisterGlobalDespawn(this);
        protected virtual void OnDisable() => _spawner.UnregisterGlobalDespawn(this);

        protected abstract void OnReleaseObjectEffect(PoolObjectBehaviour @object);

        void IPoolSpawner.OnCreate(PoolObjectBehaviour prefab) { }
        void IPoolSpawner.OnRelease(PoolObjectBehaviour prefab) => OnReleaseObjectEffect(prefab);
    }
}