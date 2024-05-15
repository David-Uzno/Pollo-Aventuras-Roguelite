using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoVariante : BasicoEnemigo
{
    public override void TomarDaño(float daño)
    {
        _vida -= daño;
        if (_vida <= 0)
        {
            Destroy(gameObject);
        }
    }
}