using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private HUD _interfazUsuario;

    public int PuntosTotales { get { return _puntosTotales; } }

    private int _puntosTotales;
    private int _cantidadVidas = 3;

    [Header("Otros")]
    [SerializeField] FPSCounter _FPSContador;

    private void Awake()
    {
        // Verificar si ya hay una instancia creada
        if (Instance == null)
        {
            // Si no hay una instancia, establecer esta como la instancia única
            Instance = this;
        }
        else
        {
            // Si ya hay una instancia, destruir este objeto y mostrar un mensaje de advertencia
            Debug.Log("¡Cuidado! Más de un ControladorJuego en la escena.");
            Destroy(gameObject);
        }
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

    void FPS()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            _FPSContador.Show();
        }
    }
}