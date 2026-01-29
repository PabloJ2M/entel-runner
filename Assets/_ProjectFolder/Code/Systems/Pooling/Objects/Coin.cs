using UnityEngine;

namespace Unity.Pool
{
    public class Coin : PoolObjectOnSpline
    {
        [SerializeField] private int _amount = 1;
        private const string _tag = "Player";

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag(_tag)) return;

            if (collision.TryGetComponent(out Collector collector))
                collector.AddPoints(_amount);
            
            Destroy();
        }
    }
}