using UnityEngine;

namespace Tutorial
{
    public class TutorialCoin : MonoBehaviour
    {
        private const string _tag = "Player";

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag(_tag)) return;

            Collector collector = collision.GetComponent<Collector>();
            if (collector != null)
            {
                collector.AddPoints(50);
            }

            Destroy(gameObject);
        }
    }
}