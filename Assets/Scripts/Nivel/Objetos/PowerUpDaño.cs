using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDaño : MonoBehaviour
{
    public float damageMultiplier = 2f; // Factor por el cual se multiplicará el daño
    public float duration = 5f; // Duración del power-up en segundos

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Comprueba si el objeto con el que colisiona tiene el tag "Player"
        if (other.CompareTag("Jugador"))
        {
            // Obtiene el componente CombateCaC del jugador
            CombateCaC combate = other.GetComponent<CombateCaC>();

            // Si el jugador tiene el componente CombateCaC, activa el power-up
            if (combate != null)
            {
                combate.AumentarDaño(damageMultiplier, duration);

                // Destruye el objeto power-up después de activarlo
                Destroy(gameObject);
            }
        }
    }
}



