using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDaño : MonoBehaviour
{
    [SerializeField] private float _dañoMultiplicado = 2f;
    [SerializeField] private float _duracion = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            // Obtiene el componente CombateCaC del jugador
            CombateCaC combate = other.GetComponent<CombateCaC>();

            // Si el jugador tiene el componente CombateCaC, activa el power-up
            if (combate != null)
            {
                combate.AumentarDaño(_dañoMultiplicado, _duracion);

                Destroy(gameObject);
            }
        }
    }
}