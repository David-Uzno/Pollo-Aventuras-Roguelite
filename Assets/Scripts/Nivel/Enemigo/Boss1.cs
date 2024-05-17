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

    [SerializeField] private Vector2 coordenadasActivacion; // Coordenadas para activar el jefe
    [SerializeField] private float rangoActivacion = 1f; // Rango de activaci�n

    private bool estaCargandoSalto;
    private float tiempoUltimoSalto;
    private bool estaActivo;


    private void Start()
    {
        if (jugador == null)
        {
            jugador = GameObject.FindGameObjectWithTag("Jugador").transform;
        }
        estaActivo = false;
    }

    private void Update()
    {
        if (!estaActivo && Vector2.Distance(jugador.position, coordenadasActivacion) <= rangoActivacion)
        {
            ActivarBoss();
        }

        if (estaActivo && !estaCargandoSalto && Time.time > tiempoUltimoSalto + tiempoCargaSalto)
        {
            StartCoroutine(CargarSalto());
        }
    }

    private void ActivarBoss()
    {
        estaActivo = true;
        gameObject.SetActive(true);
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
        float velocidadSalto = 2f;
        direccionSalto *= velocidadSalto;

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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(coordenadasActivacion, rangoActivacion); // Dibuja el rango de activaci�n en la escena
    }
}



