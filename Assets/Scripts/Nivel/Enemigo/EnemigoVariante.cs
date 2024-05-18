using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemigoVariante : BasicoEnemigo
{
    [SerializeField] private List<GameObject> _dropItems;
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
            int randomIndex = Random.Range(0, _dropItems.Count);
            GameObject itemToDrop = _dropItems[randomIndex];

            Instantiate(itemToDrop, transform.position, Quaternion.identity);
        }
    }
}