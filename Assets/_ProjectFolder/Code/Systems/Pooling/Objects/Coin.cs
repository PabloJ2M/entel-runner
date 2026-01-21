using UnityEngine;

namespace Unity.Pool
{
    public class Coin : PoolObjectOnSpline
    {
        private const string _tag = "Player";

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag(_tag)) return;
            collision.GetComponent<Collector>().AddPoints(50);
            Destroy();
        }
    }
}