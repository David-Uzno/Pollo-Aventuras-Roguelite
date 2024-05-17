using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : BasicoEnemigo
{
    [SerializeField] private List<GameObject> _dropItems; // Lista de posibles objetos a dropear
    [SerializeField] private Transform jugador; // Referencia al jugador
    [SerializeField] private float tiempoCargaSalto; // Tiempo de carga antes de cada salto
    [SerializeField] private float radioDa�o; // Radio de da�o del salto
    [SerializeField] private float da�oSalto; // Da�o causado por el salto
    [SerializeField] private LayerMask capaJugador; // Capa del jugador para detectar colisiones

    private bool estaCargandoSalto;
    private float tiempoUltimoSalto;

    private void Start()
    {
        if (jugador == null)
        {
            jugador = GameObject.FindGameObjectWithTag("Jugador").transform;
        }
    }

    private void Update()
    {
        if (!estaCargandoSalto && Time.time > tiempoUltimoSalto + tiempoCargaSalto)
        {
            StartCoroutine(CargarSalto());
        }
    }

    private IEnumerator CargarSalto()
    {
        estaCargandoSalto = true;

        // Espera el tiempo de carga antes de saltar
        yield return new WaitForSeconds(tiempoCargaSalto);

        // Realiza el salto
        RealizarSalto();
        tiempoUltimoSalto = Time.time;
        estaCargandoSalto = false;
    }

    private void RealizarSalto()
    {
        // Calcula la direcci�n hacia el jugador
        Vector2 direccionSalto = (jugador.position - transform.position).normalized;

        // Salta a la posici�n del jugador
        transform.position = (Vector2)transform.position + direccionSalto;

        // Causa da�o en el �rea de efecto
        Collider2D[] objetosAfectados = Physics2D.OverlapCircleAll(transform.position, radioDa�o, capaJugador);
        foreach (Collider2D colisionador in objetosAfectados)
        {
            
        }
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

    public override void DropItem()
    {
        if (_dropItems.Count > 0)
        {
            // Selecciona un objeto aleatorio de la lista
            int randomIndex = Random.Range(0, _dropItems.Count);
            GameObject itemToDrop = _dropItems[randomIndex];

            // Instancia el objeto en la posici�n del enemigo
            Instantiate(itemToDrop, transform.position, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioDa�o); 
    }
}


