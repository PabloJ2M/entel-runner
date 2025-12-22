using UnityEngine;

namespace Unity.Pool
{
    public class SpawnerPointRandom : SpawnerPoint
    {
        [SerializeField, Range(0, 1)] private float _threshold;
        [SerializeField] private LayerMask _mask;

        protected override void OnSpawn()
        {
            if (Random.value < _threshold) return;
            if (Physics2D.OverlapCircle(transform.position, 1f, _mask)) return;
            base.OnSpawn();
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, 1f);
        }
    }
}