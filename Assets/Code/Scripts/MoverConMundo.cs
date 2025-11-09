using UnityEngine;

public class MoverConMundo : MonoBehaviour
{
    [SerializeField] private CintaTransportadora mundo;

    void Update()
    {
        if (mundo == null) return;
        transform.position += mundo.DesplazamientoDelta;
    }

}
