using UnityEngine;

namespace Unity.Pool
{
    [RequireComponent(typeof(IPoolManagerObjects))]
    public sealed class PoolObjectDisplacement : MonoBehaviour
    {
        private IPoolManagerObjects _manager;

        private void Awake() => _manager = GetComponent<IPoolManagerObjects>();
        private void OnEnable()
        {
            var gameManager = GameplayManager.Instance;
            gameManager.onDinstanceTraveled += MoveDistance;
            gameManager.onFixedMovement += FixedDistance;
        }

        private void MoveDistance(float worldDistance)
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