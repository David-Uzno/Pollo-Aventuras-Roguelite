using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrullar : MonoBehaviour
{
    [SerializeField] private float _velocidadMovimiento;
    [SerializeField] private Transform[] _puntosPatrulla;
    [SerializeField] private float _distanciaMinima;

    private int _puntoActual;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _puntoActual = Random.Range(0, _puntosPatrulla.Length);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Girar();
    }
   
    private void Update()
    {
        MoverHaciaPunto();
        VerificarDistancia();
    }

    private void MoverHaciaPunto()
    {
        transform.position = Vector2.MoveTowards(transform.position, _puntosPatrulla[_puntoActual].position, _velocidadMovimiento * Time.deltaTime);
    }

    private void VerificarDistancia()
    {
        if (Vector2.Distance(transform.position, _puntosPatrulla[_puntoActual].position) < _distanciaMinima)
        {
            _puntoActual = Random.Range(0, _puntosPatrulla.Length);
            Girar();
        }
    }

    private void Girar()
    {
        if (transform.position.x < _puntosPatrulla[_puntoActual].position.x)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
    }
}