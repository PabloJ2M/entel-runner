using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Unity.Pool
{
    public class SpawnerPointRandom : SpawnerPoint
    {
        [SerializeField, Range(0, 1)] private float _threshold;
        [SerializeField] private LayerMask _mask;
        [SerializeField] private float _minPlayerY;
        [SerializeField] private float _maxPlayerY = 6f;
        private Transform _player;

        protected override void Awake()
        {
            base.Awake();
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        protected override void OnSpawn()
        {
            float playerY = _player.position.y;
            if (_player.position.y < _minPlayerY) return;
            if (playerY < _minPlayerY || playerY > _maxPlayerY) return;
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