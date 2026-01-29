using UnityEngine;

namespace Unity.Pool
{
    public class CollectCoinsEffect : PoolManagerParticles
    {
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private Transform _player;

        protected override void OnReleaseObjectEffect(PoolObjectBehaviour @object)
        {
            if (@object is not Coin coin) return;
            if (coin.Transform.PositionX() < _player.PositionX()) return;

            var particle = Pool.Get() as CoinParticle;
            particle.SetParticleReference(_particle);
            particle.SetPosition(coin.Transform.position, _particle.transform.position);
        }
    }
}