using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private float vida;
  
    
   
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
            GameManager.Instancia.PerderVida();
        }
    }
}
