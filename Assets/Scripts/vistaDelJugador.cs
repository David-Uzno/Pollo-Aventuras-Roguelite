using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vistaDelJugador : MonoBehaviour
{
    public Transform jugador; // Referencia al transform del jugador

    void Update()
    {
        if (jugador != null)
        {
            // Obtener la posición actual de la cámara
            Vector3 posicionCamara = transform.position;

            // Obtener la posición actual del jugador
            Vector3 posicionJugador = jugador.position;

            // Mantener la misma posición en Z de la cámara
            posicionCamara.z = -10f;

            // Asignar la posición del jugador a la cámara con un desfase (offset)
            transform.position = posicionJugador + new Vector3(0f, 0f, posicionCamara.z);
        }
    }
}


