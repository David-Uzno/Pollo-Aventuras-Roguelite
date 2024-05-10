using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour

{
    public Transform Jugador; // Referencia al transform del jugador
    public float velocidad = 5f; // Velocidad de movimiento del enemigo

    void Update()
    {
        if (Jugador != null)
        {
            // Calcular la dirección hacia el jugador
            Vector3 direccion = Jugador.position - transform.position;
            direccion.Normalize(); // Normalizar para obtener la dirección correcta

            // Mover el enemigo hacia el jugador
            transform.Translate(direccion * velocidad * Time.deltaTime);

            // Rotar el enemigo hacia la dirección del jugador (opcional)
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direccion);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.Instance.PerderVida();
        }
    }
}


