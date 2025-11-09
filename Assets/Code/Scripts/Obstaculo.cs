using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    [SerializeField] private string tagMortal = "Mortal";
    void Reset()
    {
        gameObject.tag = tagMortal;
    }
}
