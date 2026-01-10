using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    [SerializeField] private float _speed = 12f;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent(out DeathCondition deathScript))
            {
                deathScript.Disable();
            }
            Destroy(gameObject);
        }
    }
}