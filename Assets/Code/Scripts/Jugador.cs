using UnityEngine;
using UnityEngine.InputSystem;

public class Jugador : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float fuerzaJetpack = 14f;
    [SerializeField] private float limiteAlturaSuperior = 8f;
    [SerializeField] private float limiteAlturaInferior = -4f;
    [SerializeField] private float aceleracionSubida = 55f;
    [SerializeField] private float velocidadMaxSubida = 12f;
    [SerializeField] private float velocidadMaxBajada = -18f;
    [SerializeField] private float multiplicadorCaida = 2.4f;
    [SerializeField] private float multiplicadorSoltar = 2.0f;

    [Header("Referencias")]
    [SerializeField] private Rigidbody2D cuerpo;
    [SerializeField] private CintaTransportadora mundo;
    [SerializeField] private AdministradorJuego admin;

    private Controles controles;    
    private bool presionando;

    void Reset()
    {
        cuerpo = GetComponent<Rigidbody2D>();
    }

    void Awake()
    {
        if (!cuerpo) cuerpo = GetComponent<Rigidbody2D>();
        controles = new Controles();
    }

    void OnEnable()
    {
        controles.Juego.MantenerVuelo.performed += OnMantenerVuelo;
        controles.Juego.MantenerVuelo.canceled += OnSoltarVuelo;
        controles.Enable();
    }

    void OnDisable()
    {
        controles.Juego.MantenerVuelo.performed -= OnMantenerVuelo;
        controles.Juego.MantenerVuelo.canceled -= OnSoltarVuelo;
        controles.Disable();
    }

    private void OnMantenerVuelo(InputAction.CallbackContext ctx) => presionando = true;
    private void OnSoltarVuelo(InputAction.CallbackContext ctx) => presionando = false;

    void Update()
    {
        if (admin != null && admin.Estado != EstadoJuego.Jugando) return;
        var pos = transform.position;
        if (pos.y > limiteAlturaSuperior) pos.y = limiteAlturaSuperior;
        transform.position = pos;
    }

    void FixedUpdate()
    {
        if (admin != null && admin.Estado != EstadoJuego.Jugando) return;

        Vector2 v = cuerpo.linearVelocity;

        if (presionando)
        {
            v.y = Mathf.MoveTowards(v.y, velocidadMaxSubida, aceleracionSubida * Time.fixedDeltaTime);
        }
        else
        {
            float mult = v.y > 0f ? multiplicadorSoltar : multiplicadorCaida;
            v.y += Physics2D.gravity.y * (mult - 1f) * Time.fixedDeltaTime;

            if (v.y < velocidadMaxBajada) v.y = velocidadMaxBajada;
        }

        cuerpo.linearVelocity = new Vector2(cuerpo.linearVelocity.x, v.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Mortal") ||
            other.collider.gameObject.layer == LayerMask.NameToLayer("Obstaculos"))
            admin?.MatarJugador();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mortal")) admin?.MatarJugador();
        if (other.CompareTag("Monedas")) admin?.SumarMoneda(1);
    }
}
