using UnityEngine;

namespace Unity.Pool
{
    public class Coin : PoolObjectOnSpline
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            
            collision.GetComponent<Collector>().AddPoints(50);
            Destroy();
        }
    }
}