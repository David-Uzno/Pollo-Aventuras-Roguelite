using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instancia { get; private set; }

    [SerializeField] private HUD _interfazUsuario;

    public int PuntosTotales { get { return _puntosTotales; } }

    private int _puntosTotales;
    private int _cantidadVidas = 3;

    [Header("Otros")]
    [SerializeField] FPSCounter _FPSContador;

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("¡Cuidado! Más de un ControladorJuego en la escena.");
            Destroy(gameObject);
        }
    }

    public void GanarJuego()
    {
        SceneManager.LoadScene("Ganar");
    }

    public void SumarPuntos(int puntosASumar)
    {
        _puntosTotales += puntosASumar;
        Debug.Log("Puntos totales: " + _puntosTotales);
        _interfazUsuario.ActualizarPuntos(_puntosTotales);
    }

    public void PerderVida()
    {
        _cantidadVidas--;
        _interfazUsuario.DesactivarVida(_cantidadVidas);
    }

    public void RecuperarVida()
    {
        _interfazUsuario.ActivarVida(_cantidadVidas);
        _cantidadVidas++;
    }

    public void IrAlMenu(string Menu)
    {
        if (string.IsNullOrEmpty(Menu))
        {
            Debug.LogError("El nombre de la escena no puede estar vacío o ser nulo.");
            return;
        }

        if (!Application.CanStreamedLevelBeLoaded(Menu))
        {
            Debug.LogError("La escena '" + Menu + "' no existe o no está incluida en la lista de escenas de construcción.");
            return;
        }

        try
        {
            SceneManager.LoadScene(Menu);
            Debug.Log("Cargando la escena: " + Menu);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error al cargar la escena '" + Menu + "': " + e.Message);
        }
    }

    void FPS()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            _FPSContador.Show();
        }
    }
}
