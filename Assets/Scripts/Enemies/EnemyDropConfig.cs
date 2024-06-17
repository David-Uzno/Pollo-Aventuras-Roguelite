using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyDropConfig", menuName = "Enemy/DropConfig", order = 0)]
public class EnemyDropConfig : ScriptableObject
{
    
    [System.Serializable] public class DropItem
    {
        public GameObject item;
        [Range(0, 100)] public float dropChance;
    }
    public List<DropItem> dropItems;
}