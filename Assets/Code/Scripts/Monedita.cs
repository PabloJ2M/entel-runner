using UnityEngine;

public class Monedita : MonoBehaviour
{
    [SerializeField] private CintaTransportadora mundo;

    private void OnEnable()
    {
        if (GetComponent<MoverConMundo>() == null)
            gameObject.AddComponent<MoverConMundo>().SendMessage("set_mundo", mundo, SendMessageOptions.DontRequireReceiver);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            gameObject.SetActive(false);
        }

        Destroy(gameObject);
    }
}
