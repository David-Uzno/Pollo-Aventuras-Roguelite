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
            // Obtener la posici�n actual de la c�mara
            Vector3 posicionCamara = transform.position;

            // Obtener la posici�n actual del jugador
            Vector3 posicionJugador = jugador.position;

            // Mantener la misma posici�n en Z de la c�mara
            posicionCamara.z = -10f;

            // Asignar la posici�n del jugador a la c�mara con un desfase (offset)
            transform.position = posicionJugador + new Vector3(0f, 0f, posicionCamara.z);
        }
    }
}


