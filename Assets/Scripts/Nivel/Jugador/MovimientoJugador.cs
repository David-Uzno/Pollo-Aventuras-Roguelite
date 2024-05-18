using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    #region Variables
    [Header("Movimiento")]
    [SerializeField] private float _velocidadMovimiento = 5f;
    
    [Header("Dash")]
    [SerializeField] private float _velocidadDash = 10f;
    [SerializeField] private float _cooldownDash = 0.5f;

    [Header("Estado")]
    [SerializeField] private int _cantidadVidas = 3;
    [SerializeField] private bool _puedeRealizarDash = true;
    private float _tiempoUltimoDash;

    [Header("Animator")]
    [SerializeField] private Animator _animator;
    [SerializeField] private float _duracionParaAnimacionInactivo = 2f;
    private float _tiempoInactivo;
    #endregion

    #region Unity Métodos
    private void Update()
    {
        Movimiento();
        ProcesarDash();
        VerificarInactividad();
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
    private void Movimiento()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(movimientoHorizontal, movimientoVertical, 0f) * _velocidadMovimiento * Time.deltaTime;

        MoverJugador(movimiento);
        RotarJugador(movimientoHorizontal);

        RegistrarTiempoInactividad(movimiento);
    }

    private void MoverJugador(Vector3 movimiento)
    {
        transform.Translate(movimiento, Space.World);
        _animator.SetBool("Walk", true);
    }

    private void RotarJugador(float movimientoHorizontal)
    {
        if (movimientoHorizontal < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (movimientoHorizontal > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void RegistrarTiempoInactividad(Vector3 movimiento)
    {
        if (movimiento != Vector3.zero)
        {
            _tiempoInactivo = 0f;

            if (_animator != null && !_animator.enabled)
            {
                _animator.enabled = true;
            }
        }
        else
        {
            _tiempoInactivo += Time.deltaTime;
        }
    }
    #endregion

    #region Métodos de Dash
    private void ProcesarDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && _puedeRealizarDash)
        {
            if (Time.time > _tiempoUltimoDash + _cooldownDash)
            {
                Vector3 direccionDash = ObtenerDireccionDash();
                AplicarDash(direccionDash);

                _tiempoUltimoDash = Time.time;
                _puedeRealizarDash = false;
                StartCoroutine(ReactivarDash());
            }
        }
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
        yield return new WaitForSeconds(_cooldownDash);
        _puedeRealizarDash = true;
    }
    #endregion

    #region Animación de Inactividad
    private void VerificarInactividad()
    {
        if (_tiempoInactivo >= _duracionParaAnimacionInactivo && _animator != null && _animator.enabled)
        {
            _animator.SetBool("Walk", false);
        }
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