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
            CombateCaC combate = other.GetComponent<CombateCaC>();

            if (combate != null)
            {
                combate.AumentarDaño(_dañoMultiplicado, _duracion);

                Destroy(gameObject);
            }
        }
    }
}