using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPJugador : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Teletransportador"))
        {
            Teletransportador teleporter = otro.GetComponent<Teletransportador>();

            if (teleporter != null)
            {
                Vector3 teleportCoordinates = teleporter.CoordenadasTeletransporte;
                transform.position = teleportCoordinates;

                Camera.main.transform.position = new Vector3(teleportCoordinates.x, teleportCoordinates.y, Camera.main.transform.position.z);
            }
        }
    }
}