using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPJugador : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otro)
    {
        // Comprueba si el objeto con el que colisiona tiene el tag "Teleporter"
        if (otro.CompareTag("Teleporter"))
        {
            // Obtiene el componente Teleporter del objeto con el que colisiona
            Teletransportador teleporter = otro.GetComponent<Teletransportador>();

            // Si el objeto tiene el componente Teleporter, se teletransporta
            if (teleporter != null)
            {
                // Teletransporta al jugador a las coordenadas del teletransportador
                Vector3 teleportCoordinates = teleporter.CoordenadasTeletransporte;
                transform.position = teleportCoordinates;

                // Teletransporta la cámara principal a las mismas coordenadas que el jugador
                Camera.main.transform.position = new Vector3(teleportCoordinates.x, teleportCoordinates.y, Camera.main.transform.position.z);
            }
        }
    }
}