using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlayer : MonoBehaviour
{
    void Start()
    {
        int _indexPlayer = PlayerPrefs.GetInt("PlayerIndex");
        Instantiate(CharacterManager.Instance.CharacterIndex[_indexPlayer]._player, transform.position, Quaternion.identity);
    }
}