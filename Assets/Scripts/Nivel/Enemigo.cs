using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private float vida;
    [SerializeField] private Transform _seguirJugador;
    [SerializeField] private float _velocidadMovimiento = 5f;
    [SerializeField] private Vector2 _limitesMovimientoMin = new Vector2(-5f, -5f); // Límites mínimos de movimiento
    [SerializeField] private Vector2 _limitesMovimientoMax = new Vector2(5f, 5f); // Límites máximos de movimiento

    private void Update()
    {
        if (_seguirJugador != null)
        {
            // Calcular la dirección hacia el jugador
            Vector3 direccion = _seguirJugador.position - transform.position;
            direccion.Normalize(); // Normalizar para obtener la dirección correcta

            // Mover el enemigo hacia el jugador
            Vector3 newPosition = transform.position + direccion * _velocidadMovimiento * Time.deltaTime;

            // Aplicar límites de movimiento
            newPosition.x = Mathf.Clamp(newPosition.x, _limitesMovimientoMin.x, _limitesMovimientoMax.x);
            newPosition.y = Mathf.Clamp(newPosition.y, _limitesMovimientoMin.y, _limitesMovimientoMax.y);

            transform.position = newPosition;

            // Rotar el enemigo hacia la dirección del jugador (opcional)
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direccion);
        }
    }
    public void TomarDaño(float daño)
    {
        vida -= daño;
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            GameManager.Instance.PerderVida();
        }
    }
}
