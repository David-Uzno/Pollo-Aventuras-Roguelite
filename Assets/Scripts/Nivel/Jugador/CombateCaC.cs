using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateCaC : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform _controladorGolpe;
    [SerializeField] private float _radioGolpe;
    [SerializeField] private float _dañoBaseGolpe;

    private float _dañoActualGolpe;
    #endregion

    #region Unity Métodos
    private void Start()
    {
        _dañoActualGolpe = InicializarDaño(_dañoBaseGolpe);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Golpe(_dañoActualGolpe);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_controladorGolpe.position, _radioGolpe);
    }
    #endregion

    #region Métodos de Golpe
    private void Golpe(float daño)
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(_controladorGolpe.position, _radioGolpe);
        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemigo"))
            {
                BasicoEnemigo enemigo = colisionador.transform.GetComponent<BasicoEnemigo>();
                if (enemigo != null)
                {
                    enemigo.TomarDaño(daño);
                }
            }
        }
    }
    #endregion

    #region Métodos de Power-Up
    public void AumentarDaño(float multiplicador, float duracion)
    {
        StartCoroutine(ActivarPowerUp(multiplicador, duracion));
    }

    private IEnumerator ActivarPowerUp(float multiplicador, float duracion)
    {
        float dañoModificado = _dañoBaseGolpe * multiplicador;
        _dañoActualGolpe = AjustarDaño(dañoModificado, duracion);

        yield return new WaitForSeconds(duracion);

        _dañoActualGolpe = _dañoBaseGolpe;
    }
    #endregion

    #region Métodos de Utilidad
    private float InicializarDaño(float dañoBase)
    {
        return dañoBase;
    }

    private float AjustarDaño(float dañoModificado, float duracion)
    {
        return dañoModificado;
    }
    #endregion
}