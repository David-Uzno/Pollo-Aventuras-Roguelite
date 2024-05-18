using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicoEnemigo : MonoBehaviour
{
    [SerializeField] public float _vida;
    [SerializeField] private float _ataque;
    [SerializeField] private float _velocidad;
    [SerializeField] private List<GameObject> Recompensa = new List<GameObject>();
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
        sr.color = basico;
    }
    public abstract void TomarDaño(float daño);
    public abstract void DropItem();

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            GameManager.Instancia.PerderVida();
        }
    }
   
}