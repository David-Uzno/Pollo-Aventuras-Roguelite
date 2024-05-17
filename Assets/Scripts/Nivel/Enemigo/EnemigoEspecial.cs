using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoEspecial : BasicoEnemigo
{
    [SerializeField] private float _vida;
    [SerializeField] private List<GameObject> _dropItems;
    public Transform controladorDisparo;
    public float distanciaLinea;
    public LayerMask capaJugador;
    public bool jugadorEnRango;
    public GameObject BalaEnemigo;
    public float tiempoEntreDisparos;
    public float tiempoUltimoDisparo;
    public float tiempoEsperaDisparo;
    public float velocidadDisparo;
    public float velocidadRotacion;


    private void Update()
    {
        
        MoverControladorDisparo();
        RotarControladorDisparo();

     
        jugadorEnRango = Physics2D.Raycast(controladorDisparo.position, transform.right, distanciaLinea, capaJugador);
        if (jugadorEnRango)
        {
            if (Time.time > tiempoUltimoDisparo + tiempoEntreDisparos)
            {
                tiempoUltimoDisparo = Time.time;
                Invoke(nameof(Disparar), tiempoEsperaDisparo);
            }
        }
    }
    private void MoverControladorDisparo()
    {
      
        controladorDisparo.position += transform.right * velocidadDisparo * Time.deltaTime;
    }
    private void RotarControladorDisparo()
    {
        
        controladorDisparo.Rotate(Vector3.forward, velocidadRotacion * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorDisparo.position, controladorDisparo.position + transform.right *  distanciaLinea);
    }

    public override void TomarDa�o(float da�o)
    {
        _vida -= da�o;
        if (_vida <= 0)
        {
            DropItem();
            Destroy(gameObject);
        }
    }
    private void Disparar()
    {
        Instantiate(BalaEnemigo, controladorDisparo.position,controladorDisparo.rotation);
    }
    public override void DropItem()
    {
        if (_dropItems.Count > 0)
        {
            int randomIndex = Random.Range(0, _dropItems.Count);
            GameObject itemToDrop = _dropItems[randomIndex];

            Instantiate(itemToDrop, transform.position, Quaternion.identity);
        }
    }

}
