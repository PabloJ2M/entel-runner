using UnityEngine;
using System.Collections.Generic;

public class PoolObjetos : MonoBehaviour
{
    [System.Serializable]
    public class Entrada
    {
        public GameObject prefab;
        public int cantidadInicial = 8;
    }

    [SerializeField] private List<Entrada> entradas = new();
    private readonly Dictionary<GameObject, Queue<GameObject>> pools = new();

    void Awake()
    {
        foreach (var e in entradas)
        {
            if (!pools.ContainsKey(e.prefab))
                pools[e.prefab] = new Queue<GameObject>();

            for (int i = 0; i < e.cantidadInicial; i++)
            {
                var go = Instantiate(e.prefab, transform);
                go.SetActive(false);
                pools[e.prefab].Enqueue(go);
            }
        }
    }

    public GameObject Tomar(GameObject prefab, Vector3 posicion, Quaternion rotacion)
    {
        if (!pools.TryGetValue(prefab, out var cola))
        {
            cola = new Queue<GameObject>();
            pools[prefab] = cola;
        }

        GameObject go = cola.Count > 0 ? cola.Dequeue() : Instantiate(prefab, transform);
        go.transform.SetPositionAndRotation(posicion, rotacion);
        go.SetActive(true);
        return go;
    }

    public void Devolver(GameObject prefab, GameObject instancia)
    {
        instancia.SetActive(false);
        if (!pools.TryGetValue(prefab, out var cola))
        {
            cola = new Queue<GameObject>();
            pools[prefab] = cola;
        }
        cola.Enqueue(instancia);
    }
}
