using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicoEnemigo : MonoBehaviour
{
    public float _vida;
    [SerializeField] private float _ataque;
    [SerializeField] private float _velocidad;
    [SerializeField] private List<GameObject> Recompensa = new List<GameObject>();
    public Color _colorBasico;
    public SpriteRenderer _spriteRenderer;
    public IEnumerator Damage()
    {
        _spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = _colorBasico;
    }
    private void Start()
    {
        _spriteRenderer.color = _colorBasico;
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