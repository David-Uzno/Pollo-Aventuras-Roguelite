using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateCaC : MonoBehaviour
{
    [SerializeField] private Transform _controladorGolpe;
    [SerializeField] private float _radioGolpe;
    [SerializeField] private float _dañoGolpe;

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
        foreach (Collider2D colicionador in objetos)
        {
            if (colicionador.CompareTag("Enemigo"))
            {
                // Cambia Enemigo por EnemigoVariante
                EnemigoVariante enemigo = colicionador.transform.GetComponent<EnemigoVariante>();
                if (enemigo != null)
                {
                    enemigo.TomarDaño(_dañoGolpe);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_controladorGolpe.position, _radioGolpe);
    }
}