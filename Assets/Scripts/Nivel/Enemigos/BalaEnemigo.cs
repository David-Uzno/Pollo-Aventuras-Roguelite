using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{
    [SerializeField] private float _velocidad;
    [SerializeField] private int _daño;
    
    void Update()
    {
        transform.Translate(Time.deltaTime * _velocidad * Vector2.right);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Jugador jugador))
        {
            jugador.PerderVida();
            Destroy(gameObject);

        }
        if (other.gameObject.CompareTag("Jugador"))
        {
            GameManager.Instancia.PerderVida();
        }
    }
}