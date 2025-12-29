using UnityEngine;

namespace Unity.Pool
{
    public class SpawnerPointRandom : SpawnerPoint
    {
        [SerializeField] private float minPlayerY;
        [SerializeField] private float maxPlayerY;

        [SerializeField, Range(0, 1)] private float threshold = 0.1f;

        [SerializeField] private LayerMask mask;

        [SerializeField] private Vector2 forwardCheckSize = new Vector2(8f, 2.5f);
        [SerializeField] private Vector2 forwardCheckOffset = new Vector2(4f, 0f);

        private Transform player;

        protected override void Awake()
        {
            base.Awake();
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        protected override void OnSpawn()
        {
            if (player == null) return;
            float y = player.position.y;
            if (y < minPlayerY || y > maxPlayerY) return;
            if (Random.value < threshold) return;
            Vector2 checkPos = (Vector2)transform.position + forwardCheckOffset;
            if (Physics2D.OverlapBox(checkPos, forwardCheckSize, 0f, mask))
                return;
            base.OnSpawn();
        }
    }
}
