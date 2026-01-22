using UnityEngine;

namespace Tutorial
{
    public class TutorialAutoMover : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private float destroyPositionX = -20f;

        private void Update()
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);

            if (transform.position.x < destroyPositionX)
            {
                Destroy(gameObject);
            }
        }

        public void SetSpeed(float newSpeed)
        {
            speed = newSpeed;
        }
    }
}