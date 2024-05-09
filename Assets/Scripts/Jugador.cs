using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad de movimiento

    void Update()
    {
        // Obtener las entradas de teclado para el movimiento
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        // Calcular el vector de movimiento basado en las entradas
        Vector3 movimiento = new Vector3(movimientoHorizontal, movimientoVertical, 0f) * velocidad * Time.deltaTime;

        // Si el movimiento es diagonal, normalizar el vector de movimiento para evitar el derrape
        if (movimientoHorizontal != 0 && movimientoVertical != 0)
        {
            movimiento = movimiento.normalized * velocidad * Time.deltaTime;
        }

        // Aplicar el movimiento al objeto
        transform.Translate(movimiento);
    }
}






