using System.Collections;
using UnityEngine;

namespace Unity.Pool
{
    public class CoinParticle : PoolParticle
    {
        [SerializeField] private float _speed = 1f;
        private Vector2 _origin, _target;
        private ParticleSystem _particle;

        public void SetParticleReference(ParticleSystem particle)
        {
            _particle = particle;
        }
        public void SetPosition(Vector2 position, Vector2 target)
        {
            _origin = Transform.position = position;
            _target = target;

            StartCoroutine(CollectEffect());
        }

        protected IEnumerator CollectEffect()
        {
            float time = 0f;

            while (time < 1f)
            {
                yield return null;
                time += Time.deltaTime * _speed;
                Transform.position = Vector2.Lerp(_origin, _target, time);
            }

            _particle?.Play();
            Destroy();
        }
    }
}