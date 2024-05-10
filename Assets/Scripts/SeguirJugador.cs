using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirJugador : MonoBehaviour
{
    [SerializeField] private Transform _transformacionJugador;

    private void Update()
    {
        if (_transformacionJugador != null)
        {
            // Obtener la posición actual de la cámara
            Vector3 posicionCamara = transform.position;

            // Obtener la posición actual del jugador
            Vector3 posicionJugador = _transformacionJugador.position;

            // Mantener la misma posición en Z de la cámara
            posicionCamara.z = -10f;

            // Asignar la posición del jugador a la cámara con un desfase (offset)
            transform.position = posicionJugador + new Vector3(0f, 0f, posicionCamara.z);
        }
    }
}