using UnityEngine;

namespace Unity.Pool
{
    public class CollectCoinsEffect : PoolParticlesGrabEffect<Coin>
    {
        [SerializeField] private ParticleSystem _particle;

        protected override void Spawn(Coin reference)
        {
            var particle = Pool.Get() as CoinParticle;
            particle.SetParticleReference(_particle);
            particle.SetPosition(reference.Transform.position, _particle.transform.position);
        }
    }
}