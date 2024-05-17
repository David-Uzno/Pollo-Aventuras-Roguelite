using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corazon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.gameObject.CompareTag("Jugador"))
        {
            // Aumentar la vida del jugador
            Jugador jugador = otro.GetComponent<Jugador>();
            if (jugador != null)
            {
                jugador.RecuperarVida();
            }

            // Notificar al GameManager
            GameManager.Instancia.RecuperarVida();

            // Destruir el objeto del corazón
            Destroy(gameObject);
        }
    }
}