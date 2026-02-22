using UnityEngine;

namespace Unity.Pool
{
    public abstract class PoolParticlesGrabEffect<T> : PoolManagerParticles where T : PoolObjectOnSpline
    {
        [SerializeField] private Transform _player;
        [SerializeField] private float _threshold = 1f;

        protected override void OnReleaseObjectEffect(PoolObjectBehaviour @object)
        {
            if (@object is not T element) return;
            if (element.Transform.PositionX() < _player.PositionX() - _threshold) return;

            Spawn(element);
        }

        protected abstract void Spawn(T reference);
    }
}