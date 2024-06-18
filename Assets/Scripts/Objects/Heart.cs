using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private int _recoveryAmount = 1;

    public delegate void HeartCollected();
    public static event HeartCollected OnHeartCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>()?.RecoverLife(_recoveryAmount);

            NotifyHeartCollection();

            Destroy(gameObject);
        }
    }
    private void NotifyHeartCollection()
    {
        if (OnHeartCollected != null)
        {
            OnHeartCollected.Invoke();
        }
    }
}