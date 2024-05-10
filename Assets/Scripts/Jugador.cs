using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float velocidadMovimiento = 5f; // Velocidad de movimiento del jugador
    public float velocidadDash = 10f; // Velocidad de dash del jugador
    public float duracionDash = 0.5f; // Duraci�n del dash en segundos
    public int vidas = 3; // Cantidad inicial de vidas
    public bool puedeDash = true; // Indica si el jugador puede realizar un dash
    private float tiempoUltimoDash; // Tiempo en el que se realiz� el �ltimo dash

    void Update()
    {
        // Obtener las entradas de teclado para el movimiento
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        // Calcular el vector de movimiento basado en las entradas
        Vector3 movimiento = new Vector3(movimientoHorizontal, movimientoVertical, 0f) * velocidadMovimiento * Time.deltaTime;

        // Aplicar el movimiento al jugador
        transform.Translate(movimiento);

        // Verificar si se ha presionado la tecla de dash y si el jugador puede realizar un dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && puedeDash)
        {
            Dash();
        }
    }

    void Dash()
    {
        // Verificar si ha pasado suficiente tiempo desde el �ltimo dash
        if (Time.time > tiempoUltimoDash + duracionDash)
        {
            // Obtener la direcci�n de dash basada en las entradas de teclado
            Vector3 direccionDash = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f).normalized;

            // Aplicar el impulso al jugador en la direcci�n del dash
            transform.position += direccionDash * velocidadDash * Time.deltaTime;

            // Actualizar el tiempo del �ltimo dash
            tiempoUltimoDash = Time.time;

            // Desactivar la capacidad de hacer dash durante un tiempo
            puedeDash = false;

            // Iniciar una corrutina para reactivar la capacidad de hacer dash despu�s de un cierto tiempo
            StartCoroutine(ReactivarDash());
        }
    }

    IEnumerator ReactivarDash()
    {
        // Esperar durante la duraci�n del dash
        yield return new WaitForSeconds(duracionDash);

        // Reactivar la capacidad de hacer dash
        puedeDash = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            PerderVida(); // Llama al m�todo PerderVida
            Debug.Log("�Te han destruido!");
            // Aqu� puedes agregar m�s acciones como reiniciar el nivel o mostrar un mensaje de Game Over
        }
    }

    // M�todo para perder una vida
    void PerderVida()
    {
        vidas--; // Disminuye la cantidad de vidas
        Debug.Log("Vidas restantes: " + vidas);

        if (vidas <= 0)
        {
            // Si las vidas son igual o menor a cero, destruye el jugador
            Destroy(gameObject);
            Debug.Log("�Has perdido todas tus vidas! Game Over.");
            // Aqu� puedes agregar m�s acciones como reiniciar el nivel o mostrar un mensaje de Game Over
        }
    }
}











