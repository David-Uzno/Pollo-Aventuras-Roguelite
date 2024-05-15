using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoVariante : BasicoEnemigo
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AtaqueEspecial();
            TomarDaño();
        }
    }

    public override void AtaqueEspecial()
    {
        Debug.Log($"Ataque Especial de Variante");
    }
}