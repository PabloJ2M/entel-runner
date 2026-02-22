using UnityEngine;

namespace Unity.Pool
{
    public class CoinParticle : LerpParticle
    {
        private ParticleSystem _particle;

        public void SetParticleReference(ParticleSystem particle) => _particle = particle;
        protected override void OnReachTarget() => _particle?.Play();
    }
}