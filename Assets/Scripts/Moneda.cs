using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    [SerializeField] private int _valorMoneda = 1;
    [SerializeField] private GameManager _controladorJuego;

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.CompareTag("Jugador"))
        {
            _controladorJuego.SumarPuntos(_valorMoneda);
            Destroy(gameObject);
        }
    }
}