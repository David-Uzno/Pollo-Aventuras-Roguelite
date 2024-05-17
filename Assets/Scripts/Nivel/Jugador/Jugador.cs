using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    [Header("Configuración del Jugador")]
    [SerializeField] private float _velocidadMovimiento = 5f;
    [SerializeField] private float _velocidadDash = 10f;
    [SerializeField] private float _duracionDash = 0.5f;

    [Header("Estado del Jugador")]
    [SerializeField] private int _cantidadVidas = 3;
    [SerializeField] private bool _puedeRealizarDash = true;
    private float _tiempoUltimoDash;

    private void Update()
    {
        // Obtener las entradas de teclado para el movimiento
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        // Calcular el vector de movimiento basado en las entradas
        Vector3 movimiento = new Vector3(movimientoHorizontal, movimientoVertical, 0f) * _velocidadMovimiento * Time.deltaTime;

        // Aplicar el movimiento al jugador
        transform.Translate(movimiento);

        // Verificar si se ha presionado la tecla de dash y si el jugador puede realizar un dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && _puedeRealizarDash)
        {
            EjecutarDash();
        }
    }

    private void EjecutarDash()
    {
        // Verificar si ha pasado suficiente tiempo desde el último dash
        if (Time.time > _tiempoUltimoDash + _duracionDash)
        {
            // Obtener la dirección de dash basada en las entradas de teclado
            Vector3 direccionDash = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f).normalized;

            // Aplicar el impulso al jugador en la dirección del dash
            transform.position += direccionDash * _velocidadDash * Time.deltaTime;

            // Actualizar el tiempo del último dash
            _tiempoUltimoDash = Time.time;

            // Desactivar la capacidad de hacer dash durante un tiempo
            _puedeRealizarDash = false;

            // Iniciar una corrutina para reactivar la capacidad de hacer dash después de un cierto tiempo
            StartCoroutine(ReactivarDash());
        }
    }

    private IEnumerator ReactivarDash()
    {
        // Esperar durante la duración del dash
        yield return new WaitForSeconds(_duracionDash);

        // Reactivar la capacidad de hacer dash
        _puedeRealizarDash = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo"))
        {
            PerderVida(); // Llama al método PerderVida
            Debug.Log("¡Te han destruido!");
        }
    }

    public void PerderVida()
    {
        _cantidadVidas--; // Disminuye la cantidad de vidas
        Debug.Log("Vidas restantes: " + _cantidadVidas);

        if (_cantidadVidas <= 0)
        {
            // Si las vidas son igual o menor a cero, destruye el jugador
            Destroy(gameObject);
            Debug.Log("¡Has perdido todas tus vidas! Game Over.");
        }
    }
    
    public void RecuperarVida()
    {
        _cantidadVidas++; // Aumenta la cantidad de vidas
        Debug.Log("¡Has recuperado una vida! Vidas restantes: " + _cantidadVidas);
    }
}