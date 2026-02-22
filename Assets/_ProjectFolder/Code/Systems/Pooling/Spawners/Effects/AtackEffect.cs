using UnityEngine;

namespace Unity.Pool
{
    public class AtackEffect : PoolParticlesGrabEffect<KnockBack>
    {
        [SerializeField] private Health _boss;
        [SerializeField] private float _height;

        private Vector3 _distance;

        protected override void Awake()
        {
            base.Awake();
            _distance = _height * Vector3.up;
        }

        protected override void Spawn(KnockBack reference)
        {
            var particle = Pool.Get() as KnockBackParticle;
            particle.SetPosition(reference.Transform.position, _boss.transform.position + _distance);
            particle.SetActionAtTarget(RemoveHealthPoint);
        }

        private void RemoveHealthPoint()
        {
            _boss.RemoveHealth(1);
        }
    }
}