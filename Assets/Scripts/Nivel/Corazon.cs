using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corazon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.gameObject.CompareTag("Jugador"))
        {
            GameManager.Instance.RecuperarVida();
            Destroy(gameObject);
        }
    }
}