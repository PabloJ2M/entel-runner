using UnityEngine;

namespace Unity.Pool
{
    public class KnockBack : Obstacle2D
    {
        private const string _player = "Player";

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(_player))
            {

            }
        }
    }
}