using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private Transform _transformacionJugador;
    [SerializeField] private float _velocidadMovimiento = 5f;

    private void Update()
    {
        if (_transformacionJugador != null)
        {
            // Calcular la dirección hacia el jugador
            Vector3 direccion = _transformacionJugador.position - transform.position;
            direccion.Normalize(); // Normalizar para obtener la dirección correcta

            // Mover el enemigo hacia el jugador
            transform.Translate(direccion * _velocidadMovimiento * Time.deltaTime);

            // Rotar el enemigo hacia la dirección del jugador (opcional)
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direccion);
        }
    }

    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.gameObject.CompareTag("Jugador"))
        {
            GameManager.Instancia.PerderVida();
        }
    }
}