using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : FatherObject
{
    [SerializeField] private int _recoveryAmount = 1;

    public delegate void HeartCollected();
    public static event HeartCollected OnHeartCollected;

    protected override void ExecuteAction(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.RecoverLife(_recoveryAmount);
        }
        NotifyHeartCollection();
    }

    private void NotifyHeartCollection()
    {
        OnHeartCollected?.Invoke();
    }
}