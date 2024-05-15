using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicoEnemigo : MonoBehaviour
{
    [SerializeField] private float Vida;
    [SerializeField] private float Daño;
    [SerializeField] private float Velocidad;
    [SerializeField] private List<GameObject> Recompensa = new List<GameObject>();

    void Update()
    {
        Debug.Log("Update");
    }

    public void Ataque()
    {
        Debug.Log("Ataque");
    }

    public abstract void AtaqueEspecial();

    public void TomarDaño()
    {
        Debug.Log("Tomar Daño");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            GameManager.Instancia.PerderVida();
        }
    }
}