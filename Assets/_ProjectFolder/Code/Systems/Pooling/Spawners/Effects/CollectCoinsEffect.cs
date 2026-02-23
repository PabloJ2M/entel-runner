using UnityEngine;

namespace Unity.Pool
{
    public class CollectCoinsEffect : PoolParticlesGrabEffect<Coin>
    {
        [SerializeField] private ParticleSystem _particle;

        protected override void Spawn(Coin reference) => SpawnParticleAt(reference.Transform);

        public void SpawnParticleAt(Transform transform) => SpawnParticleAt(transform.position);
        public void SpawnParticleAt(Vector2 position)
        {
            var particle = Pool.Get() as CoinParticle;
            particle.SetParticleReference(_particle);
            particle.SetPosition(position, _particle.transform.position);
        }
    }
}