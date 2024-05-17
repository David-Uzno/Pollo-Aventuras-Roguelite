using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    #region Variables
    [Header("Configuración del Jugador")]
    [SerializeField] private float _velocidadMovimiento = 5f;
    [SerializeField] private float _velocidadDash = 10f;
    [SerializeField] private float _duracionDash = 0.5f;

    [Header("Estado del Jugador")]
    [SerializeField] private int _cantidadVidas = 3;
    [SerializeField] private bool _puedeRealizarDash = true;
    private float _tiempoUltimoDash;
    #endregion

    #region Unity Métodos
    private void Update()
    {
        ProcesarMovimiento();
        ProcesarDash();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo"))
        {
            PerderVida();
            Debug.Log("¡Te han destruido!");
        }
    }
    #endregion

    #region Métodos de Movimiento
    private void ProcesarMovimiento()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        Vector3 movimiento = CalcularMovimiento(movimientoHorizontal, movimientoVertical);
        MoverJugador(movimiento);
    }

    private Vector3 CalcularMovimiento(float horizontal, float vertical)
    {
        return new Vector3(horizontal, vertical, 0f) * _velocidadMovimiento * Time.deltaTime;
    }

    private void MoverJugador(Vector3 movimiento)
    {
        transform.Translate(movimiento);
    }
    #endregion

    #region Métodos de Dash
    private void ProcesarDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && _puedeRealizarDash)
        {
            EjecutarDash();
        }
    }

    private void EjecutarDash()
    {
        if (PuedeRealizarDash())
        {
            Vector3 direccionDash = ObtenerDireccionDash();
            AplicarDash(direccionDash);
            _tiempoUltimoDash = Time.time;
            _puedeRealizarDash = false;
            StartCoroutine(ReactivarDash());
        }
    }

    private bool PuedeRealizarDash()
    {
        return Time.time > _tiempoUltimoDash + _duracionDash;
    }

    private Vector3 ObtenerDireccionDash()
    {
        return new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f).normalized;
    }

    private void AplicarDash(Vector3 direccion)
    {
        transform.position += direccion * _velocidadDash * Time.deltaTime;
    }

    private IEnumerator ReactivarDash()
    {
        yield return new WaitForSeconds(_duracionDash);
        _puedeRealizarDash = true;
    }
    #endregion

    #region Métodos de Vida
    public void PerderVida()
    {
        _cantidadVidas--;
        Debug.Log("Vidas restantes: " + _cantidadVidas);

        if (_cantidadVidas <= 0)
        {
            DestruirJugador();
        }
    }

    private void DestruirJugador()
    {
        Destroy(gameObject);
        Debug.Log("¡Has perdido todas tus vidas! Game Over.");
    }

    public void RecuperarVida()
    {
        _cantidadVidas++;
        Debug.Log("¡Has recuperado una vida! Vidas restantes: " + _cantidadVidas);
    }
    #endregion
}