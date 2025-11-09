using UnityEngine;

public class Seccion : MonoBehaviour
{
    [SerializeField] private Transform marcadorFinal; 
    public float Ancho => marcadorFinal ? marcadorFinal.localPosition.x : 23f;
    public float BordeDerechoMundo => marcadorFinal ? marcadorFinal.position.x : transform.position.x + 23f;
}

