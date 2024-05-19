using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Jefe : BasicoEnemigo
{
    [SerializeField] private List<GameObject> _dropItems; 
    [SerializeField] private Transform _jugador; 
    [SerializeField] private float _tiempoCargaSalto; 
    [SerializeField] private float _radioDaño; 
    [SerializeField] private float _dañoSalto; 
    [SerializeField] private LayerMask _capaJugador; 

    [SerializeField] private Vector2 _coordenadasActivacion; 
    [SerializeField] private float _rangoActivacion = 1f; 

    private bool _estaCargandoSalto;
    private float _tiempoUltimoSalto;
    private bool _estaActivo;
   

    private void Start()
    {
        if (_jugador == null)
        {
            _jugador = GameObject.FindGameObjectWithTag("Jugador").transform;
        }
        _estaActivo = false;
       
    }

    private void Update()
    {
        if (!_estaActivo && Vector2.Distance(_jugador.position, _coordenadasActivacion) <= _rangoActivacion)
        {
            ActivarBoss();
        }

        if (_estaActivo && !_estaCargandoSalto && Time.time > _tiempoUltimoSalto + _tiempoCargaSalto)
        {
            StartCoroutine(CargarSalto());
        }
    }

    private void ActivarBoss()
    {
        _estaActivo = true;
        gameObject.SetActive(true);
    }

    private IEnumerator CargarSalto()
    {
        _estaCargandoSalto = true;

        yield return new WaitForSeconds(_tiempoCargaSalto);

        RealizarSalto();
        _tiempoUltimoSalto = Time.time;
        _estaCargandoSalto = false;
    }

    private void RealizarSalto()
    {

        Vector2 direccionSalto = (_jugador.position - transform.position).normalized;
        float velocidadSalto = 2f;
        direccionSalto *= velocidadSalto;

        transform.position = (Vector2)transform.position + direccionSalto;

        Collider2D[] objetosAfectados = Physics2D.OverlapCircleAll(transform.position, _radioDaño, _capaJugador);
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
        Gizmos.DrawWireSphere(transform.position, _radioDaño);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_coordenadasActivacion, _rangoActivacion); 
    }
}