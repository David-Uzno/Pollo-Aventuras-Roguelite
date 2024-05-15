using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrullar : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private Transform[] puntosMovimientos;
    [SerializeField] private float distanciaMinima;

    private int numeroAleatorio;
    private SpriteRenderer spriteRender;

    private void Start()
    {
        numeroAleatorio = Random.Range(0,puntosMovimientos.Length);
        spriteRender = GetComponent<SpriteRenderer>();
        Girar();
    }

   
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, puntosMovimientos[numeroAleatorio].position,velocidadMovimiento * Time.deltaTime);
        if (Vector2.Distance(transform.position, puntosMovimientos[numeroAleatorio].position) < distanciaMinima)
        {
            numeroAleatorio = Random.Range(0,puntosMovimientos.Length);
            Girar();
        }
    }
    private void Girar()
    {
        if(transform.position.x < puntosMovimientos[numeroAleatorio].position.x)
        {
            spriteRender.flipX = true;
        }
        else
        {
            spriteRender.flipY = false;
        }
    }
}
