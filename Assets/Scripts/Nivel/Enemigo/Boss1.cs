using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Boss1 : BasicoEnemigo
{
    [SerializeField] private List<GameObject> _dropItems; 
    [SerializeField] private Transform jugador; 
    [SerializeField] private float tiempoCargaSalto; 
    [SerializeField] private float radioDaño; 
    [SerializeField] private float dañoSalto; 
    [SerializeField] private LayerMask capaJugador; 

    [SerializeField] private Vector2 coordenadasActivacion; 
    [SerializeField] private float rangoActivacion = 1f; 

    private bool estaCargandoSalto;
    private float tiempoUltimoSalto;
    private bool estaActivo;
    public Color basico;
    public SpriteRenderer sr;
    IEnumerator damage()
    {
        sr.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        sr.color = basico;
    }

    private void Start()
    {
        if (jugador == null)
        {
            jugador = GameObject.FindGameObjectWithTag("Jugador").transform;
        }
        estaActivo = false;
        sr.color = basico;
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

        yield return new WaitForSeconds(tiempoCargaSalto);

        RealizarSalto();
        tiempoUltimoSalto = Time.time;
        estaCargandoSalto = false;
    }

    private void RealizarSalto()
    {

        Vector2 direccionSalto = (jugador.position - transform.position).normalized;
        float velocidadSalto = 2f;
        direccionSalto *= velocidadSalto;

        transform.position = (Vector2)transform.position + direccionSalto;

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
            GameManager.Instancia.GanarJuego();
        }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioDaño);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(coordenadasActivacion, rangoActivacion); 
    }
}



