using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FatherObject : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ExecuteAction(collision);
            Destroy(gameObject);
        }
    }

    protected abstract void ExecuteAction(Collider2D collision);
}