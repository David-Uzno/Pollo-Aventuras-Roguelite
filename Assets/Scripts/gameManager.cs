using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour

{
    // Instancia estática de la clase
    public static gameManager Instance { get; private set; }

    // Referencia al HUD
    public HUD hud;

    // Propiedad para acceder a los puntos totales
    public int PuntosTotales { get { return puntosTotales; } }

    private int puntosTotales;
    private int vidas = 3;

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
            Debug.Log("¡Cuidado! Más de un GameManager en la escena.");
            Destroy(gameObject);
        }
    }

    // Método para sumar puntos
    public void SumarPuntos(int puntosASumar)
    {
        puntosTotales += puntosASumar;
        Debug.Log("Puntos totales: " + puntosTotales);
        hud.ActualizarPuntos(puntosTotales);
    }

    // Método para perder una vida
    public void PerderVida()
    {
        vidas--;
        hud.DesactivarVida(vidas);
    }
    public void RecuperarVida()
    {
        hud.ActivarVida(vidas);
        vidas += 1;
    }
}

