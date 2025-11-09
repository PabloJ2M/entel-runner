using UnityEngine;

public class CintaTransportadora : MonoBehaviour
{
    [SerializeField] private float velocidad = 8f; 
    public float Velocidad => velocidad;

    public Vector3 DesplazamientoDelta => Vector3.left * velocidad * Time.deltaTime;
}
