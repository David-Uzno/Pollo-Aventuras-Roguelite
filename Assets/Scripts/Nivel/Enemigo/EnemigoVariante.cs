using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemigoVariante : BasicoEnemigo
{
    [SerializeField] private List<GameObject> _dropItems; // Lista de posibles objetos a dropear

    public override void TomarDaño(float daño)
    {
        _vida -= daño;
        if (_vida <= 0)
        {
            DropItem();
            Destroy(gameObject);
        }
    }

    public override void DropItem()
    {
        if (_dropItems.Count > 0)
        {
            // Selecciona un objeto aleatorio de la lista
            int randomIndex = Random.Range(0, _dropItems.Count);
            GameObject itemToDrop = _dropItems[randomIndex];

            // Instancia el objeto en la posición del enemigo
            Instantiate(itemToDrop, transform.position, Quaternion.identity);
        }
    }
}