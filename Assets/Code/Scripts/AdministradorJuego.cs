using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum EstadoJuego { Preparado, Jugando, Pausado, Muerto }
public class AdministradorJuego : MonoBehaviour
{
    [Header("Estado")]
    [SerializeField] private EstadoJuego estado = EstadoJuego.Preparado;
    public EstadoJuego Estado => estado;

    [Header("UI")]
    [SerializeField] private TMP_Text textoMonedas;
    [SerializeField] private GameObject panelMuerte;

    private int monedas;

    void Start()
    {
        CambiarEstado(EstadoJuego.Jugando);
        ActualizarUI();
        if (panelMuerte) panelMuerte.SetActive(false);
    }

    public void SumarMoneda(int cantidad)
    {
        monedas += cantidad;
        ActualizarUI();
    }

    public void MatarJugador()
    {
        if (estado == EstadoJuego.Muerto) return;
        CambiarEstado(EstadoJuego.Muerto);
        Time.timeScale = 0f; 
        if (panelMuerte) panelMuerte.SetActive(true);
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    private void CambiarEstado(EstadoJuego nuevo)
    {
        estado = nuevo;
    }

    private void ActualizarUI()
    {
        if (textoMonedas) textoMonedas.text = monedas.ToString();
    }
}
