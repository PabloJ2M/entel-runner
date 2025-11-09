using UnityEngine;
using System.Collections.Generic;

public class GeneradorSecciones : MonoBehaviour
{
    [Header("Secciones")]
    [SerializeField] private List<GameObject> prefabsSeccion = new();
    [SerializeField] private int cantidadInicial = 3;

    [Header("Spawn")]
    [SerializeField] private Transform puntoInicio;    
    [SerializeField] private float margenPreSpawn = 8f; 
    [SerializeField] private float cooldownSpawn = 0.15f; 

    [Header("Referencias")]
    [SerializeField] private Camera camara;
    [SerializeField] private CintaTransportadora mundo;

    private readonly List<GameObject> activas = new();
    private float siguienteX;      
    private float proximoTiempoSpawn;
    private float bordeDerechoCamara;

    void Start()
    {
        ActualizarBordeDerechoCamara();

        if (!puntoInicio)
        {
            var go = new GameObject("PuntoInicio");
            puntoInicio = go.transform;
            puntoInicio.position = new Vector3(bordeDerechoCamara + 8f, 0f, 0f);
        }

        siguienteX = puntoInicio.position.x;

        for (int i = 0; i < cantidadInicial; i++)
            InstanciarSeccionEnSiguienteX();
    }

    void Update()
    {
        ActualizarBordeDerechoCamara();

        if (Time.time >= proximoTiempoSpawn && activas.Count > 0)
        {
            var ultima = activas[activas.Count - 1];
            var bordeUltima = ultima.GetComponent<Seccion>().BordeDerechoMundo;

            if (bordeUltima < bordeDerechoCamara + margenPreSpawn)
            {
                InstanciarSeccionEnSiguienteX();
                proximoTiempoSpawn = Time.time + cooldownSpawn;
            }
        }

        float umbralIzquierdo = camara.transform.position.x - 50f;
        for (int i = activas.Count - 1; i >= 0; i--)
        {
            if (activas[i].GetComponent<Seccion>().BordeDerechoMundo < umbralIzquierdo)
            {
                Destroy(activas[i]); 
                activas.RemoveAt(i);
            }
        }
    }

    private void InstanciarSeccionEnSiguienteX()
    {
        var prefab = prefabsSeccion[Random.Range(0, prefabsSeccion.Count)];

        var temp = prefab.GetComponent<Seccion>();
        float ancho = temp ? temp.Ancho : 25f;

        Vector3 pos = new Vector3(siguienteX, puntoInicio.position.y, 0f);
        var go = Instantiate(prefab, pos, Quaternion.identity);

        if (!go.TryGetComponent(out MoverConMundo mover))
            mover = go.AddComponent<MoverConMundo>();
        var campo = typeof(MoverConMundo).GetField("mundo",
            System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
        campo?.SetValue(mover, mundo);

        activas.Add(go);

        siguienteX += ancho;
    }

    private void ActualizarBordeDerechoCamara()
    {
        float half = camara.orthographicSize * camara.aspect;
        bordeDerechoCamara = camara.transform.position.x + half;
    }
}
