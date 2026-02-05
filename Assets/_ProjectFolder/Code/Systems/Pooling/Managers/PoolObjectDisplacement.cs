using UnityEngine;

namespace Unity.Pool
{
    [RequireComponent(typeof(IPoolManagerObjects))]
    public sealed class PoolObjectDisplacement : GameplayListener
    {
        private IPoolDisplaceObjects _manager;

        protected override void Awake()
        {
            base.Awake();
            _manager = GetComponent<IPoolDisplaceObjects>();
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            _gameManager.onFixedMovement += FixedDistance;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            _gameManager.onFixedMovement -= FixedDistance;
        }

        protected override void GameUpdate(float worldDistance)
        {
            float world = worldDistance * _manager.SpeedMultiply + _manager.WorldOffset;

            for (int i = _manager.Spawned.Count - 1; i >= 0; i--)
                _manager.Spawned[i].RefreshPosition(world);
        }
        private void FixedDistance()
        {
            for (int i = _manager.Spawned.Count - 1; i >= 0; i--)
                _manager.Spawned[i].FixedInterpolation();
        }
    }
}