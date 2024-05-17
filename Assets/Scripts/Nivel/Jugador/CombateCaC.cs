using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateCaC : MonoBehaviour
{
    [SerializeField] private Transform _controladorGolpe;
    [SerializeField] private float _radioGolpe;
    [SerializeField] private float _dañoBaseGolpe;

    private float _dañoActualGolpe;

    private void Start()
    {
        _dañoActualGolpe = _dañoBaseGolpe;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Golpe();
        }
    }

    private void Golpe()
    {
       
        Collider2D[] objetos = Physics2D.OverlapCircleAll(_controladorGolpe.position, _radioGolpe);
        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemigo") || colisionador.CompareTag("EnemigoEspecial") || colisionador.CompareTag("Boss"))
            {
                BasicoEnemigo vidaEnemigo = colisionador.GetComponent<BasicoEnemigo>();
                if (vidaEnemigo != null)
                {
                    vidaEnemigo.TomarDaño(_dañoActualGolpe);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_controladorGolpe.position, _radioGolpe);
    }

    public void AumentarDaño(float multiplicador, float duracion)
    {
        StartCoroutine(ActivarPowerUp(multiplicador, duracion));
    }

    private IEnumerator ActivarPowerUp(float multiplicador, float duracion)
    {
        _dañoActualGolpe = _dañoBaseGolpe * multiplicador;
        yield return new WaitForSeconds(duracion);
        _dañoActualGolpe = _dañoBaseGolpe;
    }
}
