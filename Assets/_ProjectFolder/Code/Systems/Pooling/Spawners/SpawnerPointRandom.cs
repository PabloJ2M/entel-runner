using UnityEngine;

namespace Unity.Pool
{
    public class SpawnerPointRandom : SpawnerPoint
    {
        [SerializeField, Range(0, 1)] private float _threshold = 0.1f;

        [SerializeField] private Transform player;
        [SerializeField] private LayerMask _mask;

        [SerializeField] private float _minPlayerY;
        [SerializeField] private float _maxPlayerY;
        [SerializeField] private Vector2 _forwardCheckSize = new(8f, 2.5f);
        [SerializeField] private Vector2 _forwardCheckOffset = new(4f, 0f);

        protected override void OnSpawn()
        {
            if (Random.value > _threshold) return;

            float yPosition = player.PositionY();
            if (yPosition < _minPlayerY || yPosition > _maxPlayerY) return;

            Vector2 checkPos = (Vector2)transform.position + _forwardCheckOffset;
            if (Physics2D.OverlapBox(checkPos, _forwardCheckSize, 0f, _mask)) return;

            base.OnSpawn();
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Vector2 checkPos = (Vector2)transform.position + _forwardCheckOffset;

            Gizmos.color = Color.orange;
            Gizmos.DrawWireCube(checkPos, _forwardCheckSize);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(new Vector2(0, _minPlayerY), 0.2f);
            Gizmos.DrawSphere(new Vector2(0, _maxPlayerY), 0.2f);
        }
        #endif
    }
}