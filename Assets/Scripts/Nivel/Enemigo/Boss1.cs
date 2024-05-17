using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : BasicoEnemigo
{
    [SerializeField] private List<GameObject> _dropItems; // Lista de posibles objetos a dropear
    [SerializeField] private Transform jugador; // Referencia al jugador
    [SerializeField] private float tiempoCargaSalto; // Tiempo de carga antes de cada salto
    [SerializeField] private float radioDaño; // Radio de daño del salto
    [SerializeField] private float dañoSalto; // Daño causado por el salto
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
        // Calcula la dirección hacia el jugador
        Vector2 direccionSalto = (jugador.position - transform.position).normalized;

        // Salta a la posición del jugador
        transform.position = (Vector2)transform.position + direccionSalto;

        // Causa daño en el área de efecto
        Collider2D[] objetosAfectados = Physics2D.OverlapCircleAll(transform.position, radioDaño, capaJugador);
        foreach (Collider2D colisionador in objetosAfectados)
        {
            
        }
    }

    public override void TomarDaño(float daño)
    {
        _vida -= daño;
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

            // Instancia el objeto en la posición del enemigo
            Instantiate(itemToDrop, transform.position, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioDaño); 
    }
}


