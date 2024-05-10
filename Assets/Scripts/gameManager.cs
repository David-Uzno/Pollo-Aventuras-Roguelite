using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour

{
    // Instancia est�tica de la clase
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
            // Si no hay una instancia, establecer esta como la instancia �nica
            Instance = this;
        }
        else
        {
            // Si ya hay una instancia, destruir este objeto y mostrar un mensaje de advertencia
            Debug.Log("�Cuidado! M�s de un GameManager en la escena.");
            Destroy(gameObject);
        }
    }

    // M�todo para sumar puntos
    public void SumarPuntos(int puntosASumar)
    {
        puntosTotales += puntosASumar;
        Debug.Log("Puntos totales: " + puntosTotales);
        hud.ActualizarPuntos(puntosTotales);
    }

    // M�todo para perder una vida
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

